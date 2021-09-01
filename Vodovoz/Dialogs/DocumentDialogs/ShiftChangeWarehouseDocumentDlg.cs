﻿using System;
using System.Collections.Generic;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QSOrmProject;
using QS.Validation;
using Vodovoz.Additions.Store;
using Vodovoz.Domain.Documents;
using Vodovoz.PermissionExtensions;
using QS.DomainModel.Entity.EntityPermissions.EntityExtendedPermission;
using Vodovoz.Domain.Goods;
using System.Linq;
using Vodovoz.Infrastructure.Report.SelectableParametersFilter;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate;
using Vodovoz.ViewModels.Reports;
using Vodovoz.ReportsParameters;
using Gamma.GtkWidgets;
using QS.Project.Services;
using QSProjectsLib;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.EntityRepositories.Stock;
using Vodovoz.Parameters;
using Vodovoz.Domain.Permissions.Warehouses;

namespace Vodovoz.Dialogs.DocumentDialogs
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ShiftChangeWarehouseDocumentDlg : QS.Dialog.Gtk.EntityDialogBase<ShiftChangeWarehouseDocument>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		private readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();
		private readonly INomenclatureRepository _nomenclatureRepository =
			new NomenclatureRepository(new NomenclatureParametersProvider(new ParametersProvider()));
		private readonly IStockRepository _stockRepository = new StockRepository();

		private SelectableParametersReportFilter filter;

		public ShiftChangeWarehouseDocumentDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<ShiftChangeWarehouseDocument>();
			Entity.Author = _employeeRepository.GetEmployeeForCurrentUser(UoW);
			if(Entity.Author == null) {
				MessageDialogHelper.RunErrorDialog("Ваш пользователь не привязан к действующему сотруднику, вы не можете создавать складские документы, так как некого указывать в качестве кладовщика.");
				FailInitialize = true;
				return;
			}

			var storeDocument = new StoreDocumentHelper();
			if(UoW.IsNew)
				Entity.Warehouse = storeDocument.GetDefaultWarehouse(UoW, WarehousePermissionsType.ShiftChangeCreate);
			if(!UoW.IsNew)
				Entity.Warehouse = storeDocument.GetDefaultWarehouse(UoW, WarehousePermissionsType.ShiftChangeEdit);

			ConfigureDlg(storeDocument);
		}

		public ShiftChangeWarehouseDocumentDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<ShiftChangeWarehouseDocument>(id);
			
			var storeDocument = new StoreDocumentHelper();
			ConfigureDlg(storeDocument);
		}

		public ShiftChangeWarehouseDocumentDlg(ShiftChangeWarehouseDocument sub) : this (sub.Id)
		{
		}

		bool canCreate;
		bool canEdit;

		public bool CanSave => canCreate || canEdit;

		void ConfigureDlg(StoreDocumentHelper storeDocument)
		{
			canEdit = !UoW.IsNew && storeDocument.CanEditDocument(WarehousePermissionsType.ShiftChangeEdit, Entity.Warehouse);

			if(Entity.Id != 0 && Entity.TimeStamp < DateTime.Today)
			{
				var permissionValidator = 
					new EntityExtendedPermissionValidator(PermissionExtensionSingletonStore.GetInstance(), _employeeRepository);
				
				canEdit &= permissionValidator.Validate(
					typeof(ShiftChangeWarehouseDocument), ServicesConfig.UserService.CurrentUserId, nameof(RetroactivelyClosePermission));
			}

			canCreate = UoW.IsNew && !storeDocument.CheckCreateDocument(WarehousePermissionsType.ShiftChangeCreate, Entity.Warehouse);

			if(!canCreate && UoW.IsNew){
				FailInitialize = true;
				return;
			}

			if(!canEdit && !UoW.IsNew)
				MessageDialogHelper.RunWarningDialog("У вас нет прав на изменение этого документа.");

			ydatepickerDocDate.Sensitive = yentryrefWarehouse.IsEditable = ytextviewCommnet.Editable = canEdit || canCreate;

			ytreeviewNomenclatures.Sensitive = 
				buttonFillItems.Sensitive = 
				buttonAdd.Sensitive = canEdit || canCreate;

			ytreeviewNomenclatures.ItemsDataSource = Entity.ObservableItems;
			ytreeviewNomenclatures.YTreeModel?.EmitModelChanged();

			ydatepickerDocDate.Binding.AddBinding(Entity, e => e.TimeStamp, w => w.Date).InitializeFromSource();
			if(UoW.IsNew)
				yentryrefWarehouse.ItemsQuery = storeDocument.GetRestrictedWarehouseQuery(WarehousePermissionsType.ShiftChangeCreate);
			if(!UoW.IsNew)
				yentryrefWarehouse.ItemsQuery = storeDocument.GetRestrictedWarehouseQuery(WarehousePermissionsType.ShiftChangeEdit);
			yentryrefWarehouse.Binding.AddBinding(Entity, e => e.Warehouse, w => w.Subject).InitializeFromSource();
			yentryrefWarehouse.Changed += OnWarehouseChanged;

			ytextviewCommnet.Binding.AddBinding(Entity, e => e.Comment, w => w.Buffer.Text).InitializeFromSource();

			string errorMessage = "Не установлены единицы измерения у следующих номенклатур :" + Environment.NewLine;
			int wrongNomenclatures = 0;
			foreach(var item in Entity.Items) {
				if(item.Nomenclature.Unit == null) {
					errorMessage += string.Format("Номер: {0}. Название: {1}{2}",
						item.Nomenclature.Id, item.Nomenclature.Name, Environment.NewLine);
					wrongNomenclatures++;
				}
			}
			if(wrongNomenclatures > 0) {
				MessageDialogHelper.RunErrorDialog(errorMessage);
				FailInitialize = true;
				return;
			}

			filter = new SelectableParametersReportFilter(UoW);

			var nomenclatureParam = filter.CreateParameterSet(
				"Номенклатуры",
				"nomenclature",
				new ParametersFactory(UoW, (filters) => {
					SelectableEntityParameter<Nomenclature> resultAlias = null;
					var query = UoW.Session.QueryOver<Nomenclature>()
						.Where(x => !x.IsArchive);
					if(filters != null && filters.Any()) {
						foreach(var f in filters) {
							var filterCriterion = f();
							if(filterCriterion != null) {
								query.Where(filterCriterion);
							}
						}
					}

					query.SelectList(list => list
							.Select(x => x.Id).WithAlias(() => resultAlias.EntityId)
							.Select(x => x.OfficialName).WithAlias(() => resultAlias.EntityTitle)
						);
					query.TransformUsing(Transformers.AliasToBean<SelectableEntityParameter<Nomenclature>>());
					return query.List<SelectableParameter>();
				})
			);

			var nomenclatureTypeParam = filter.CreateParameterSet(
				"Типы номенклатур",
				"nomenclature_type",
				new ParametersEnumFactory<NomenclatureCategory>()
			);

			nomenclatureParam.AddFilterOnSourceSelectionChanged(nomenclatureTypeParam,
				() => {
					var selectedValues = nomenclatureTypeParam.GetSelectedValues();
					if(!selectedValues.Any()) {
						return null;
					}
					return Restrictions.On<Nomenclature>(x => x.Category).IsIn(nomenclatureTypeParam.GetSelectedValues().ToArray());
				}
			);

			//Предзагрузка. Для избежания ленивой загрузки
			UoW.Session.QueryOver<ProductGroup>().Fetch(SelectMode.Fetch, x => x.Childs).List();

			filter.CreateParameterSet(
				"Группы товаров",
				"product_group",
				new RecursiveParametersFactory<ProductGroup>(UoW,
				(filters) => {
					var query = UoW.Session.QueryOver<ProductGroup>();
					if(filters != null && filters.Any()) {
						foreach(var f in filters) {
							query.Where(f());
						}
					}
					return query.List();
				},
				x => x.Name,
				x => x.Childs)
			);

			var filterViewModel = new SelectableParameterReportFilterViewModel(filter);
			var filterWidget = new SelectableParameterReportFilterView(filterViewModel);
			vboxParameters.Add(filterWidget);
			filterWidget.Show();

			ConfigureNomenclaturesView();
		}

		private void ConfigureNomenclaturesView()
		{
			ytreeviewNomenclatures.ColumnsConfig = ColumnsConfigFactory.Create<ShiftChangeWarehouseDocumentItem>()
				.AddColumn("Номенклатура").AddTextRenderer(x => x.Nomenclature.Name)
				.AddColumn("Кол-во в учёте").AddTextRenderer(x => x.Nomenclature.Unit != null ? x.Nomenclature.Unit.MakeAmountShortStr(x.AmountInDB) : x.AmountInDB.ToString())
				.AddColumn("Кол-во по факту").AddNumericRenderer(x => x.AmountInFact).Editing()
					.Adjustment(new Gtk.Adjustment(0, 0, 10000000, 1, 10, 10))
					.AddSetter((w, x) => w.Digits = (x.Nomenclature.Unit != null ? (uint)x.Nomenclature.Unit.Digits : 1))
				.AddColumn("Разница").AddTextRenderer(x => x.Difference != 0 && x.Nomenclature.Unit != null ? x.Nomenclature.Unit.MakeAmountShortStr(x.Difference) : String.Empty)
					.AddSetter((w, x) => w.Foreground = x.Difference < 0 ? "red" : "blue")
				.AddColumn("Сумма ущерба").AddTextRenderer(x => CurrencyWorks.GetShortCurrencyString(x.SumOfDamage))
				.AddColumn("Что произошло").AddTextRenderer(x => x.Comment).Editable()
				.Finish();
		}

		public override bool Save()
		{
			if(!CanSave)
				return false;

			var valid = new QSValidator<ShiftChangeWarehouseDocument>(UoWGeneric.Root);
			if(valid.RunDlgIfNotValid((Gtk.Window)this.Toplevel))
				return false;

			Entity.LastEditor = _employeeRepository.GetEmployeeForCurrentUser(UoW);
			Entity.LastEditedTime = DateTime.Now;
			if(Entity.LastEditor == null) {
				MessageDialogHelper.RunErrorDialog("Ваш пользователь не привязан к действующему сотруднику, вы не можете изменять складские документы, так как некого указывать в качестве кладовщика.");
				return false;
			}

			logger.Info("Сохраняем акт списания...");
			UoWGeneric.Save();
			logger.Info("Ok.");
			return true;
		}

		protected void OnButtonPrintClicked(object sender, EventArgs e)
		{
			if(UoWGeneric.HasChanges && CommonDialogs.SaveBeforePrint(typeof(ShiftChangeWarehouseDocument), "акта передачи склада"))
				Save();

			var reportInfo = new QS.Report.ReportInfo {
				Title = String.Format("Акт передачи склада №{0} от {1:d}", Entity.Id, Entity.TimeStamp),
				Identifier = "Store.ShiftChangeWarehouse",
				Parameters = new Dictionary<string, object> {
					{ "document_id",  Entity.Id }
				}
			};

			TabParent.OpenTab(
				QSReport.ReportViewDlg.GenerateHashName(reportInfo),
				() => new QSReport.ReportViewDlg(reportInfo)
			);
		}

		protected void OnYentryrefWarehouseBeforeChangeByUser(object sender, EntryReferenceBeforeChangeEventArgs e)
		{
			if(Entity.Warehouse != null && Entity.Items.Count > 0) {
				if(MessageDialogHelper.RunQuestionDialog("При изменении склада табличная часть документа будет очищена. Продолжить?"))
					Entity.ObservableItems.Clear();
				else
					e.CanChange = false;
			}
		}

		#region NomenclaturesView

		protected void OnButtonFillItemsClicked(object sender, EventArgs e)
		{
			// Костыль для передачи из фильтра предназначенного только для отчетов данных в подходящем виде
			List<int> nomenclaturesToInclude = new List<int>();
			List<int> nomenclaturesToExclude = new List<int>();
			List<string> nomenclatureCategoryToInclude = new List<string>();
			List<string> nomenclatureCategoryToExclude = new List<string>();
			List<int> productGroupToInclude = new List<int>();
			List<int> productGroupToExclude = new List<int>();

			foreach (SelectableParameterSet parameterSet in filter.ParameterSets) {
				switch(parameterSet.ParameterName) {
					case "nomenclature":
						if (parameterSet.FilterType == SelectableFilterType.Include) {
							foreach (SelectableEntityParameter<Nomenclature> value in parameterSet.OutputParameters.Where(x => x.Selected)) {
								nomenclaturesToInclude.Add(value.EntityId);
							}
						} else {
							foreach(SelectableEntityParameter<Nomenclature> value in parameterSet.OutputParameters.Where(x => x.Selected)) {
								nomenclaturesToExclude.Add(value.EntityId);
							}
						}
						break;
					case "nomenclature_type":
						if(parameterSet.FilterType == SelectableFilterType.Include) {
							foreach(SelectableEnumParameter<NomenclatureCategory> value in parameterSet.OutputParameters.Where(x => x.Selected)) {
								nomenclatureCategoryToInclude.Add(value.Value.ToString());
							}
						} else {
							foreach(SelectableEnumParameter<NomenclatureCategory> value in parameterSet.OutputParameters.Where(x => x.Selected)) {
								nomenclatureCategoryToExclude.Add(value.Value.ToString());
							}
						}
						break;
					case "product_group":
						if(parameterSet.FilterType == SelectableFilterType.Include) {
							foreach(SelectableEntityParameter<ProductGroup> value in parameterSet.OutputParameters.Where(x => x.Selected)) {
								productGroupToInclude.Add(value.EntityId);
							}
						} else {
							foreach(SelectableEntityParameter<ProductGroup> value in parameterSet.OutputParameters.Where(x => x.Selected)) {
								productGroupToExclude.Add(value.EntityId);
							}
						}
						break;
				}
			}

			if(Entity.Items.Count == 0)
			{
				Entity.FillItemsFromStock(
					UoW,
					_stockRepository,
					nomenclaturesToInclude: nomenclaturesToInclude.ToArray(),
					nomenclaturesToExclude: nomenclaturesToExclude.ToArray(),
					nomenclatureTypeToInclude: nomenclatureCategoryToInclude.ToArray(),
					nomenclatureTypeToExclude: nomenclatureCategoryToExclude.ToArray(),
					productGroupToInclude: productGroupToInclude.ToArray(),
					productGroupToExclude: productGroupToExclude.ToArray());
			}
			else
			{
				Entity.UpdateItemsFromStock(
					UoW,
					_stockRepository,
					nomenclaturesToInclude: nomenclaturesToInclude.ToArray(),
					nomenclaturesToExclude: nomenclaturesToExclude.ToArray(),
					nomenclatureTypeToInclude: nomenclatureCategoryToInclude.ToArray(),
					nomenclatureTypeToExclude: nomenclatureCategoryToExclude.ToArray(),
					productGroupToInclude: productGroupToInclude.ToArray(),
					productGroupToExclude: productGroupToExclude.ToArray());
			}

			UpdateButtonState();
		}

		protected void OnButtonAddClicked(object sender, EventArgs e)
		{
			var nomenclatureSelectDlg = new OrmReference(_nomenclatureRepository.NomenclatureOfGoodsOnlyQuery());
			nomenclatureSelectDlg.Mode = OrmReferenceMode.Select;
			nomenclatureSelectDlg.ObjectSelected += NomenclatureSelectDlg_ObjectSelected;
			TabParent.AddSlaveTab(this, nomenclatureSelectDlg);
		}

		void NomenclatureSelectDlg_ObjectSelected(object sender, OrmReferenceObjectSectedEventArgs e)
		{
			var nomenclature = e.Subject as Nomenclature;
			if(Entity.Items.Any(x => x.Nomenclature.Id == nomenclature.Id))
				return;

			Entity.AddItem(nomenclature, 0, 0);
		}

		private void UpdateButtonState()
		{
			buttonFillItems.Sensitive = Entity.Warehouse != null;
			if(Entity.Items.Count == 0)
				buttonFillItems.Label = "Заполнить по складу";
			else
				buttonFillItems.Label = "Обновить остатки";
		}

		// change warehouse handler

		protected void OnWarehouseChanged(object sender, EventArgs e)
		{
			if(Entity.Warehouse != null)
				buttonFillItems.Click();
			UpdateButtonState();
		}

		#endregion
	}
}
