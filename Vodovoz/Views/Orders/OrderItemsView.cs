using System;
using System.Linq;
using Autofac;
using Gtk;
using QS.Tdi;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderItemsView : WidgetViewBase<OrderItemsViewModel>
    {
        private Widget nomenclaturesJournal;
        private Widget equipmentsJournal;

        public OrderItemsView(OrderItemsViewModel viewModel) : base (viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ybtnAddOrderItem.Clicked += (sender, args) => ViewModel.AddSalesItemCommand.Execute();
            ychkMovementEquipments.Toggled += YchkMovementEquipmentsOnToggled;
            ychkMovementEquipments.Binding.AddBinding(ViewModel, vm => vm.IsMovementItemsVisible, w => w.Active).InitializeFromSource();
            ychkDeposits.Toggled += YchkDepositsOnToggled;
            ychkDeposits.Binding.AddBinding(ViewModel, vm => vm.IsDepositsReturnsVisible, w => w.Active).InitializeFromSource();
            
            ViewModel.UpdateVisibilityNomenclaturesForSaleJournal += UpdateVisibilityNomenclaturesForSaleJournal;
            
            AddOrderDepositReturnsItemsView();
            
            ViewModel.OrderDepositReturnsItemsViewModel.UpdateVisibilityEquipmentJournal += UpdateVisibilityDepositEquipmentJournal;
        }

        private void AddOrderDepositReturnsItemsView()
        {
            var depositsView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.OrderDepositReturnsItemsViewModel);

            vboxDeposits.Add(depositsView);
            depositsView.Show();
        }

        private void UpdateVisibilityNomenclaturesForSaleJournal()
        {
            if (nomenclaturesJournal == null) {
                AddNomenclatureForSaleJournal();
            }
            else {
                ChangeJournalsVisibility(nomenclaturesJournal);
            }

            if (ViewModel.OrderInfoExpandedPanelViewModel.IsExpanded == hboxJournals.Visible) {
                ViewModel.OrderInfoExpandedPanelViewModel.Expande();
            }
        }
        
        private void AddNomenclatureForSaleJournal()
        {
            nomenclaturesJournal =
                ViewModel.AutofacScope.Resolve<ITDIWidgetResolver>()
                    .Resolve(ViewModel.NomenclaturesJournalViewModel);

            hboxJournals.Add(nomenclaturesJournal);
            nomenclaturesJournal.Show();
            hboxJournals.Show();
        }

        private void ChangeJournalsVisibility(Widget journal)
        {
            switch (hboxJournals.Visible) {
                case true:
                    journal.Visible = !journal.Visible;
                    break;
                case false:
                    hboxJournals.Visible = journal.Visible = true;
                    break;
            }

            if (hboxJournals.Visible && hboxJournals.Children.All(x => x.Visible == false)) {
                hboxJournals.Visible = false;
            }
        }

        private void UpdateVisibilityDepositEquipmentJournal()
        {
            if (equipmentsJournal == null) {
                AddDepositEquipmentJournal();
            }
            else {
                ChangeJournalsVisibility(equipmentsJournal);
            }
            
            if (ViewModel.OrderInfoExpandedPanelViewModel.IsExpanded == hboxJournals.Visible) {
                ViewModel.OrderInfoExpandedPanelViewModel.Expande();
            }
        }

        private void AddDepositEquipmentJournal()
        {
            equipmentsJournal =
                ViewModel.AutofacScope.Resolve<ITDIWidgetResolver>()
                    .Resolve(ViewModel.OrderDepositReturnsItemsViewModel.EquipmentsJournalViewModel);

            hboxJournals.Add(equipmentsJournal);
            equipmentsJournal.Show();
            hboxJournals.Show();
        }

        private void YchkDepositsOnToggled(object sender, EventArgs e)
        {
            vboxDeposits.Visible = ViewModel.IsDepositsReturnsVisible;
        }

        private void YchkMovementEquipmentsOnToggled(object sender, EventArgs e)
        {
            orderEquipmentItemsView.Visible = ViewModel.IsMovementItemsVisible;
        }

        public override void Destroy()
        {
            ViewModel.UpdateVisibilityNomenclaturesForSaleJournal -= UpdateVisibilityNomenclaturesForSaleJournal;
            ViewModel.OrderDepositReturnsItemsViewModel.UpdateVisibilityEquipmentJournal -=
                UpdateVisibilityDepositEquipmentJournal;
            base.Destroy();
        }
    }
}
