using Gamma.Utilities;
using QS.DomainModel.UoW;
using QS.ViewModels.Control.EEVM;
using QS.Views.GtkUI;
using Vodovoz.Domain.Orders;
using Vodovoz.Infrastructure.Converters;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DeliveryOrderInfoPanelView : WidgetViewBase<DeliveryOrderInfoPanelViewModel>
    {
        public DeliveryOrderInfoPanelView(DeliveryOrderInfoPanelViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ConfigureEntries();
            
            ylblStatusInfo.Binding.AddFuncBinding(ViewModel.Order, o => o.Status.GetEnumTitle(), w => w.LabelProp).InitializeFromSource();
            ylblCreationDate.Binding.AddFuncBinding(ViewModel.Order, o => o.CreateDate.HasValue ? o.CreateDate.Value.ToString("dd.MM.yyyy HH:mm") : "", w => w.LabelProp).InitializeFromSource();
            
            datePickerDeliveryDate.Binding.AddBinding(ViewModel.Order, o => o.DeliveryDate, w => w.DateOrNull).InitializeFromSource();
            pickerBillDate.Binding.AddBinding(ViewModel.Order, o => o.BillDate, w => w.Date).InitializeFromSource();
            
            enumPaymentType.Binding.AddBinding(ViewModel.Order, o => o.PaymentType, w => w.SelectedItem).InitializeFromSource();
            enumDocumentType.Binding.AddBinding(ViewModel.Order, o => o.DefaultDocumentType, w => w.SelectedItemOrNull).InitializeFromSource();
            
            entOnlineOrder.Binding.AddBinding(ViewModel.Order, o => o.OrderNumberFromOnlineStore, w => w.Text, new IntToStringConverter()).InitializeFromSource();
            
            ySpecPaymentFrom.Binding.AddBinding(ViewModel.Order, o => o.PaymentByCardFrom, w => w.SelectedItem).InitializeFromSource();
            
            ycheckAddCertificates.Binding.AddBinding(ViewModel.Order, o => o.NeedAddCertificates, w => w.Active).InitializeFromSource();
            ycheckContactlessDelivery.Binding.AddBinding(ViewModel.Order, o => o.IsContactlessDelivery, w => w.Active).InitializeFromSource();
            ycheckPaymentBySMS.Binding.AddBinding(ViewModel.Order, o => o.IsPaymentBySms, w => w.Active).InitializeFromSource();
            
            ytxtViewComment.Binding.AddBinding(ViewModel.Order, o => o.Comment, w => w.Buffer.Text).InitializeFromSource();
            ytxtViewCommentForLogist.Binding.AddBinding(ViewModel.Order, o => o.CommentForLogist, w => w.Buffer.Text).InitializeFromSource();
        }

        private void ConfigureEntries()
        {
            var counterpatryBuilder =
                new CommonEEVMBuilderFactory<DeliveryOrder>(ViewModel.ParentTab, ViewModel.Order,
                    UnitOfWorkFactory.CreateWithoutRoot(), MainClass.MainWin.NavigationManager, ViewModel.AutofacScope);

            var counterpartyViewModel = counterpatryBuilder.ForProperty(x => x.Counterparty)
                .UseViewModelJournalAndAutocompleter<CounterpartyJournalViewModel>()
                .Finish();

            counterpartyEntry.ViewModel = counterpartyViewModel;

            var deliveryPointBuilder =
                new CommonEEVMBuilderFactory<DeliveryOrder>(ViewModel.ParentTab, ViewModel.Order,
                    UnitOfWorkFactory.CreateWithoutRoot(), MainClass.MainWin.NavigationManager, ViewModel.AutofacScope);

            var deliveryPointViewModel = deliveryPointBuilder.ForProperty(x => x.DeliveryPoint)
                .UseViewModelJournalAndAutocompleter<DeliveryPointJournalViewModel>()
                .Finish();

            deliveryPointEntry.ViewModel = deliveryPointViewModel;
        }
    }
}
