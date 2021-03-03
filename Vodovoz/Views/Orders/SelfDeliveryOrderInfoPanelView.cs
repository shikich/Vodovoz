using QS.DomainModel.UoW;
using QS.ViewModels.Control.EEVM;
using QS.Views.GtkUI;
using Vodovoz.Domain.Orders;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class SelfDeliveryOrderInfoPanelView : WidgetViewBase<SelfDeliveryOrderInfoPanelViewModel>
    {
        public SelfDeliveryOrderInfoPanelView(SelfDeliveryOrderInfoPanelViewModel viewModel) : base (viewModel)
        {
            this.Build();
            Configure();
        }

        private  void Configure()
        {
            var builder =
                new CommonEEVMBuilderFactory<SelfDeliveryOrder>(ViewModel.ParentTab, ViewModel.Order,
                    UnitOfWorkFactory.CreateWithoutRoot(), MainClass.MainWin.NavigationManager, ViewModel.AutofacScope);

            var viewModel = builder.ForProperty(x => x.Counterparty)
                .UseViewModelJournalAndAutocompleter<CounterpartyJournalViewModel>()
                .Finish();

            counterpartyEntry.ViewModel = viewModel;
        }
    }
}
