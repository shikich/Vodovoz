﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using NLog;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Print;
using QS.Project.Services;
using QS.Report;
using QS.Validation.GtkUI;
using QSOrmProject;
using QSReport;
using Vodovoz.Additions.Store;
using Vodovoz.Core;
using Vodovoz.Core.Permissions;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Documents;
using Vodovoz.Domain.Store;
using Vodovoz.Repositories.HumanResources;
using Vodovoz.ViewModel;

namespace Vodovoz
{
	public partial class MovementDocumentDlg : QS.Dialog.Gtk.EntityDialogBase<MovementDocument>
	{
		static Logger logger = LogManager.GetCurrentClassLogger();

		public MovementDocumentDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<MovementDocument>();

			Entity.Author = Entity.ResponsiblePerson = EmployeeRepository.GetEmployeeForCurrentUser(UoW);
			if(Entity.Author == null) {
				MessageDialogHelper.RunErrorDialog("Ваш пользователь не привязан к действующему сотруднику, вы не можете создавать складские документы, так как некого указывать в качестве кладовщика.");
				FailInitialize = true;
				return;
			}

			Entity.FromWarehouse = StoreDocumentHelper.GetDefaultWarehouse(UoW, WarehousePermissions.MovementEdit);
			Entity.ToWarehouse = StoreDocumentHelper.GetDefaultWarehouse(UoW, WarehousePermissions.MovementEdit);

			ConfigureDlg();
		}

		public MovementDocumentDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<MovementDocument>(id);
			ConfigureDlg();
		}

		public MovementDocumentDlg(MovementDocument sub) : this(sub.Id)
		{
		}

		void ConfigureDlg()
		{
			if(StoreDocumentHelper.CheckAllPermissions(UoW.IsNew, WarehousePermissions.MovementEdit, Entity.FromWarehouse, Entity.ToWarehouse)) {
				FailInitialize = true;
				return;
			}

			textComment.Binding.AddBinding(Entity, e => e.Comment, w => w.Buffer.Text).InitializeFromSource();
			labelTimeStamp.Binding.AddBinding(Entity, e => e.DateString, w => w.LabelProp).InitializeFromSource();

			referenceCounterpartyFrom.RepresentationModel = new CounterpartyVM(new CounterpartyFilter(UoW));
			referenceCounterpartyFrom.Binding.AddBinding(Entity, e => e.FromClient, w => w.Subject).InitializeFromSource();

			referenceCounterpartyTo.RepresentationModel = new CounterpartyVM(new CounterpartyFilter(UoW));
			referenceCounterpartyTo.Binding.AddBinding(Entity, e => e.ToClient, w => w.Subject).InitializeFromSource();

			referenceWarehouseTo.ItemsQuery = StoreDocumentHelper.GetWarehouseQuery();
			referenceWarehouseTo.Binding.AddBinding(Entity, e => e.ToWarehouse, w => w.Subject).InitializeFromSource();
			referenceWarehouseFrom.ItemsQuery = StoreDocumentHelper.GetRestrictedWarehouseQuery(WarehousePermissions.MovementEdit);
			referenceWarehouseFrom.Binding.AddBinding(Entity, e => e.FromWarehouse, w => w.Subject).InitializeFromSource();
			referenceDeliveryPointTo.CanEditReference = false;
			referenceDeliveryPointTo.SubjectType = typeof(DeliveryPoint);
			referenceDeliveryPointTo.Binding.AddBinding(Entity, e => e.ToDeliveryPoint, w => w.Subject).InitializeFromSource();
			referenceDeliveryPointFrom.CanEditReference = false;
			referenceDeliveryPointFrom.SubjectType = typeof(DeliveryPoint);
			referenceDeliveryPointFrom.Binding.AddBinding(Entity, e => e.FromDeliveryPoint, w => w.Subject).InitializeFromSource();
			repEntryEmployee.RepresentationModel = new EmployeesVM();
			repEntryEmployee.Binding.AddBinding(Entity, e => e.ResponsiblePerson, w => w.Subject).InitializeFromSource();

			yentryrefWagon.SubjectType = typeof(MovementWagon);
			yentryrefWagon.Binding.AddBinding(Entity, e => e.MovementWagon, w => w.Subject).InitializeFromSource();

			ylabelTransportationStatus.Binding.AddBinding(Entity, e => e.TransportationDescription, w => w.LabelProp).InitializeFromSource();

			MovementDocumentCategory[] filteredDoctypeList = { MovementDocumentCategory.counterparty };
			object[] MovementDocumentList = Array.ConvertAll(filteredDoctypeList, x => (object)x);
			enumMovementType.ItemsEnum = typeof(MovementDocumentCategory);
			enumMovementType.AddEnumToHideList(MovementDocumentList);
			enumMovementType.Binding.AddBinding(Entity, e => e.Category, w => w.SelectedItem).InitializeFromSource();
			if(Entity.Id == 0) {
				Entity.Category = MovementDocumentCategory.Transportation;
			}

			moveingNomenclaturesView.DocumentUoW = UoWGeneric;

			UpdateAcessibility();
		}

		void UpdateAcessibility()
		{
			bool canEditOldDocument = ServicesConfig.CommonServices.PermissionService.ValidateUserPresetPermission(
				"can_edit_delivered_goods_transfer_documents",
				ServicesConfig.CommonServices.UserService.CurrentUserId
			);
			if(Entity.TransportationStatus == TransportationStatus.Delivered && !canEditOldDocument) {
				HasChanges = false;
				tableCommon.Sensitive = false;
				hbxSenderAddressee.Sensitive = false;
				moveingNomenclaturesView.Sensitive = false;
				buttonSave.Sensitive = false;
				return;
			}

			var editing = StoreDocumentHelper.CanEditDocument(WarehousePermissions.MovementEdit, Entity.FromWarehouse, Entity.ToWarehouse);
			enumMovementType.Sensitive = repEntryEmployee.IsEditable = referenceWarehouseTo.Sensitive
				 = yentryrefWagon.IsEditable = textComment.Sensitive = editing;
			moveingNomenclaturesView.Sensitive = editing;

			referenceWarehouseFrom.IsEditable = StoreDocumentHelper.CanEditDocument(WarehousePermissions.MovementEdit, Entity.FromWarehouse);

			buttonDelivered.Sensitive = Entity.TransportationStatus == TransportationStatus.Submerged
				&& CurrentPermissions.Warehouse[WarehousePermissions.MovementEdit, Entity.ToWarehouse];
		}

		public override bool Save()
		{
			var valid = new QSValidator<MovementDocument>(UoWGeneric.Root);
			if(valid.RunDlgIfNotValid((Gtk.Window)this.Toplevel))
				return false;

			Entity.LastEditor = EmployeeRepository.GetEmployeeForCurrentUser(UoW);
			Entity.LastEditedTime = DateTime.Now;
			if(Entity.LastEditor == null) {
				MessageDialogHelper.RunErrorDialog("Ваш пользователь не привязан к действующему сотруднику, вы не можете изменять складские документы, так как некого указывать в качестве кладовщика.");
				return false;
			}

			logger.Info("Сохраняем документ перемещения...");
			UoWGeneric.Save();
			logger.Info("Ok.");
			return true;
		}

		protected void OnEnumMovementTypeChanged(object sender, EventArgs e)
		{
			var selected = Entity.Category;
			referenceWarehouseTo.Visible = referenceWarehouseFrom.Visible = labelStockFrom.Visible = labelStockTo.Visible
				= (selected == MovementDocumentCategory.warehouse || selected == MovementDocumentCategory.Transportation);

			referenceCounterpartyTo.Visible = referenceCounterpartyFrom.Visible = labelClientFrom.Visible = labelClientTo.Visible
				= referenceDeliveryPointFrom.Visible = referenceDeliveryPointTo.Visible = labelPointFrom.Visible = labelPointTo.Visible
				= (selected == MovementDocumentCategory.counterparty);
			referenceDeliveryPointFrom.Sensitive = (referenceCounterpartyFrom.Subject != null && selected == MovementDocumentCategory.counterparty);
			referenceDeliveryPointTo.Sensitive = (referenceCounterpartyTo.Subject != null && selected == MovementDocumentCategory.counterparty);

			//Траспортировка
			labelWagon.Visible = hboxTransportation.Visible = yentryrefWagon.Visible = labelTransportationTitle.Visible
				= selected == MovementDocumentCategory.Transportation;
		}

		protected void OnReferenceCounterpartyFromChanged(object sender, EventArgs e)
		{
			referenceDeliveryPointFrom.Sensitive = referenceCounterpartyFrom.Subject != null;
			if(referenceCounterpartyFrom.Subject != null) {
				var points = ((Counterparty)referenceCounterpartyFrom.Subject).DeliveryPoints.Select(o => o.Id).ToList();
				referenceDeliveryPointFrom.ItemsCriteria = UoWGeneric.Session.CreateCriteria<DeliveryPoint>()
					.Add(Restrictions.In("Id", points));
			}
		}

		protected void OnReferenceCounterpartyToChanged(object sender, EventArgs e)
		{
			referenceDeliveryPointTo.Sensitive = referenceCounterpartyTo.Subject != null;
			if(referenceCounterpartyTo.Subject != null) {
				var points = ((Counterparty)referenceCounterpartyTo.Subject).DeliveryPoints.Select(o => o.Id).ToList();
				referenceDeliveryPointTo.ItemsCriteria = UoWGeneric.Session.CreateCriteria<DeliveryPoint>()
					.Add(Restrictions.In("Id", points));
			}
		}

		protected void OnButtonDeliveredClicked(object sender, EventArgs e)
		{
			buttonDelivered.Sensitive = false;
			Entity.TransportationCompleted();
		}



		protected void OnButtonPrintClicked(object sender, EventArgs e)
		{
			if(!UoWGeneric.HasChanges || CommonDialogs.SaveBeforePrint(typeof(MovementDocument), "документа") && Save()) {
				var doc = new MovementDocumentRdl(Entity);
				if(doc is IPrintableRDLDocument)
					TabParent.AddTab(DocumentPrinter.GetPreviewTab(doc as IPrintableRDLDocument), this, false);
			}
		}
	}

	public class MovementDocumentRdl : IPrintableRDLDocument
	{
		public string Title { get; set; } = "Документ перемещения";

		public MovementDocument Document { get; set; }

		public Dictionary<object, object> Parameters { get; set; }

		public PrinterType PrintType { get; set; } = PrinterType.RDL;

		public DocumentOrientation Orientation { get; set; } = DocumentOrientation.Portrait;

		public int CopiesToPrint { get; set; } = 1;

		public string Name { get; set; } = "Документ перемещения";

		public ReportInfo GetReportInfo()
		{
			return new ReportInfo {
				Title = Document.Title,
				Identifier = "Documents.MovementOperationDocucment",
				Parameters = new Dictionary<string, object>
				{
					{ "documentId" , Document.Id} ,
					{ "date" , Document.TimeStamp.ToString("dd/MM/yyyy")}
				}
			};
		}
		public MovementDocumentRdl(MovementDocument document) => Document = document;
	}
}