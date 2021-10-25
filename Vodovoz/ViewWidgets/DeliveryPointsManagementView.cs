using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QSOrmProject;
using Vodovoz.Domain.Client;
using Vodovoz.ViewModel;
using QS.Project.Services;
using QS.Tdi;
using Vodovoz.ViewModels.ViewModels.Counterparty;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DeliveryPointsManagementView : QS.Dialog.Gtk.WidgetOnDialogBase
	{
		private IUnitOfWorkGeneric<Counterparty> _deliveryPointUoW;
		public ITdiTab ParrentTab { get; set; }

		public IUnitOfWorkGeneric<Counterparty> DeliveryPointUoW
		{
			get => _deliveryPointUoW;
			set
			{
				if(_deliveryPointUoW == value)
				{
					return;
				}

				_deliveryPointUoW = value;
				if(DeliveryPointUoW.Root.DeliveryPoints == null)
				{
					DeliveryPointUoW.Root.DeliveryPoints = new List<DeliveryPoint>();
				}

				treeDeliveryPoints.RepresentationModel = new ClientDeliveryPointsVM(value);
				treeDeliveryPoints.RepresentationModel.UpdateNodes();
			}
		}

		private bool CanDelete()
		{
			return ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission(
				"can_delete_counterparty_and_deliverypoint")
			       && treeDeliveryPoints.Selection.CountSelectedRows() > 0;
		}


		public DeliveryPointsManagementView()
		{
			this.Build();

			treeDeliveryPoints.Selection.Changed += OnSelectionChanged;
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			var selected = treeDeliveryPoints.Selection.CountSelectedRows() > 0;
			buttonEdit.Sensitive = selected;
			buttonDelete.Sensitive = CanDelete();
		}

		private void OnButtonAddClicked(object sender, EventArgs e)
		{
			if(MyOrmDialog.UoW.IsNew)
			{
				if(CommonDialogs.SaveBeforeCreateSlaveEntity(MyEntityDialog.EntityObject.GetType(), typeof(DeliveryPoint)))
				{
					if(!MyTdiDialog.Save())
					{
						return;
					}
				}
				else
				{
					return;
				}
			}

			var client = DeliveryPointUoW.Root;
			MainClass.MainWin.NavigationManager.OpenViewModelOnTdi<DeliveryPointViewModel, IEntityUoWBuilder, Counterparty>(
				ParrentTab, EntityUoWBuilder.ForCreate(), client, OpenPageOptions.AsSlave);
			treeDeliveryPoints.RepresentationModel.UpdateNodes();
		}

		protected void OnButtonEditClicked(object sender, EventArgs e)
		{
			var dpId = treeDeliveryPoints.GetSelectedObject<ClientDeliveryPointVMNode>().Id;
			MainClass.MainWin.NavigationManager.OpenViewModelOnTdi<DeliveryPointViewModel, IEntityUoWBuilder>(
				ParrentTab, EntityUoWBuilder.ForOpen(dpId), OpenPageOptions.AsSlave);
		}

		protected void OnTreeDeliveryPointsRowActivated(object o, Gtk.RowActivatedArgs args)
		{
			buttonEdit.Click();
		}

		protected void OnButtonDeleteClicked(object sender, EventArgs e)
		{
			if(OrmMain.DeleteObject(typeof(DeliveryPoint), treeDeliveryPoints.GetSelectedObject<DeliveryPoint>().Id))
			{
				treeDeliveryPoints.RepresentationModel.UpdateNodes();
			}
		}
	}
}
