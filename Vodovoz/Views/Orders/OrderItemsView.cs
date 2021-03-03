using System;
using Autofac;
using Gtk;
using QS.Tdi;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderItemsView : WidgetViewBase<OrderItemsViewModel>
    {
        private Widget nomenclaturesJournal;
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
            
            ViewModel.AddJournal += AddJournal;
            ViewModel.UpdateVisibilityJournal += UpdateVisibilityJournal;
        }

        private void AddJournal()
        {
            nomenclaturesJournal =
                    ViewModel.AutofacScope.Resolve<ITDIWidgetResolver>()
                        .Resolve(ViewModel.NomenclaturesJournalViewModel);

            hboxJournals.Add(nomenclaturesJournal);
            hboxJournals.ShowAll();
            ViewModel.IsNomenclaturesJournalVisible = true;

            if (ViewModel.OrderInfoExpandedPanelViewModel.IsExpanded)
            {
                ViewModel.OrderInfoExpandedPanelViewModel.Expande();
            }
        }

        private void UpdateVisibilityJournal()
        {
            hboxJournals.Visible = ViewModel.IsNomenclaturesJournalVisible = !hboxJournals.Visible;
            
            if (ViewModel.OrderInfoExpandedPanelViewModel.IsExpanded == hboxJournals.Visible)
            {
                ViewModel.OrderInfoExpandedPanelViewModel.Expande();
            }
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
            ViewModel.AddJournal -= AddJournal;
            ViewModel.UpdateVisibilityJournal -= UpdateVisibilityJournal;
            base.Destroy();
        }
    }
}
