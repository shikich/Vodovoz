using System;
using System.ComponentModel;
using Autofac;
using Gtk;
using QS.Tdi;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [ToolboxItem(true)]
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
            
            AddOrderMovementItemsView();
            AddOrderDepositReturnsItemsView();
            
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }
        
        private void AddOrderMovementItemsView()
        {
            var movementItemsView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.OrderMovementItemsViewModel);

            vboxMovementItems.Add(movementItemsView);
            movementItemsView.Show();
        }
        
        private void AddOrderDepositReturnsItemsView()
        {
            var depositsView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.OrderDepositReturnsItemsViewModel);

            vboxDeposits.Add(depositsView);
            depositsView.Show();
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.ActiveNomenclatureJournalViewModel)) {
                UpdateJournal();
            }
        }

        private void UpdateJournal()
        {
            if (ViewModel.ActiveNomenclatureJournalViewModel is null)
            {
                CloseJournal();
                return;
            }

            AddJournal();
            
            if (ViewModel.OrderInfoExpandedPanelViewModel.IsExpanded == hboxJournals.Visible) {
                ViewModel.OrderInfoExpandedPanelViewModel.Expande();
            }
        }

        private void CloseJournal()
        {
            nomenclaturesJournal.Destroy();
            nomenclaturesJournal = null;
            hboxJournals.Visible = false;
        }

        private void AddJournal()
        {
            nomenclaturesJournal =
                ViewModel.AutofacScope.Resolve<ITDIWidgetResolver>()
                    .Resolve(ViewModel.ActiveNomenclatureJournalViewModel);

            hboxJournals.Add(nomenclaturesJournal);
            nomenclaturesJournal.Show();
            hboxJournals.Show();
        }

        private void YchkDepositsOnToggled(object sender, EventArgs e)
        {
            vboxDeposits.Visible = ViewModel.IsDepositsReturnsVisible;
        }

        private void YchkMovementEquipmentsOnToggled(object sender, EventArgs e)
        {
            vboxMovementItems.Visible = ViewModel.IsMovementItemsVisible;
        }

        public override void Destroy()
        {
            ViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
            base.Destroy();
        }
    }
}
