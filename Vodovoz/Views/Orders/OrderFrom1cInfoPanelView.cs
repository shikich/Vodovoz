using Gamma.Utilities;
using Vodovoz.JournalViewModels;
using Vodovoz.Domain.Orders;
using QS.ViewModels.Control.EEVM;
using QS.DomainModel.UoW;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.Dialogs.Orders;
using Vodovoz.Domain.Client;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderFrom1cInfoPanelView : WidgetViewBase<OrderFrom1cInfoPanelViewModel>
    {
        public OrderFrom1cInfoPanelView(OrderFrom1cInfoPanelViewModel viewModel) : base(viewModel)
        {
            Build();
            Configure();
        }

        private void Configure()
        {
            ConfigureEntries();

            ylblOrderAuthor.Binding.AddFuncBinding(
                ViewModel.Order, o => o.Author == null ? "" : o.Author.ShortName, w => w.LabelProp).InitializeFromSource();
            ylblOrderAuthor.Binding.AddBinding(ViewModel, vm => vm.IsAuthorVisible, w => w.Visible).InitializeFromSource();
            ylblAuthor.Binding.AddBinding(ViewModel, vm => vm.IsAuthorVisible, w => w.Visible).InitializeFromSource();
            ylblStatusInfo.Binding.AddFuncBinding(ViewModel.Order, o => o.Status.GetEnumTitle(), w => w.LabelProp).InitializeFromSource();
            ylblCreationDate.Binding.AddFuncBinding(ViewModel.Order,
                o => o.CreateDate.HasValue ? o.CreateDate.Value.ToString("dd.MM.yyyy HH:mm") : "", w => w.LabelProp).InitializeFromSource();
            ylblCreationDate.Binding.AddFuncBinding(ViewModel, vm => vm.IsCreationDateVisible, w => w.Visible).InitializeFromSource();
            ylblCreated.Binding.AddFuncBinding(ViewModel, vm => vm.IsCreationDateVisible, w => w.Visible).InitializeFromSource();
            ylblBillDate.Binding.AddBinding(ViewModel, vm => vm.IsBillDateVisible, w => w.Visible).InitializeFromSource();
            ylblDeliveryTime.Binding.AddBinding(ViewModel, vm => vm.IsDeliveryScheduleVisible, w => w.Visible).InitializeFromSource();

            datePickerDeliveryDate.Binding.AddBinding(ViewModel.Order, o => o.DeliveryDate, w => w.DateOrNull).InitializeFromSource();
            
            pickerBillDate.Binding.AddBinding(ViewModel.Order, o => o.BillDate, w => w.Date).InitializeFromSource();
            pickerBillDate.Binding.AddBinding(ViewModel, vm => vm.IsBillDateVisible, w => w.Visible).InitializeFromSource();
            pickerBillDate.Binding.AddBinding(ViewModel, vm => vm.IsBillDateSensitive, w => w.Sensitive).InitializeFromSource();

            enumPaymentType.ItemsEnum = typeof(PaymentType);
            enumPaymentType.Binding.AddBinding(ViewModel.Order, o => o.PaymentType, w => w.SelectedItem).InitializeFromSource();
            enumPaymentType.Binding.AddBinding(ViewModel, vm => vm.IsPaymentTypeSensitive, w => w.Sensitive).InitializeFromSource();

            referenceDeliverySchedule.Binding.AddBinding(ViewModel, vm => vm.IsDeliveryScheduleVisible, w => w.Visible).InitializeFromSource();

            ycheckAddCertificates.Binding.AddBinding(ViewModel.Order, o => o.NeedAddCertificates, w => w.Active).InitializeFromSource();
            ycheckAddCertificates.Binding.AddBinding(ViewModel, vm => vm.IsNeedAddCertificatesSensitive, w => w.Sensitive).InitializeFromSource();
            ycheckContactlessDelivery.Binding.AddBinding(ViewModel.Order, o => o.IsContactlessDelivery, w => w.Active).InitializeFromSource();
            ycheckContactlessDelivery.Binding.AddBinding(ViewModel, vm => vm.IsContactlessDeliverySensitive, w => w.Sensitive).InitializeFromSource();
            ycheckPaymentBySMS.Binding.AddBinding(ViewModel.Order, o => o.IsPaymentBySms, w => w.Active).InitializeFromSource();
            ycheckPaymentBySMS.Binding.AddBinding(ViewModel, vm => vm.IsPaymentBySMSVisible, w => w.Visible).InitializeFromSource();
            ycheckPaymentBySMS.Binding.AddBinding(ViewModel, vm => vm.IsPaymentBySMSSensitive, w => w.Sensitive).InitializeFromSource();
            
            ytxtViewComment.Binding.AddBinding(ViewModel.Order, o => o.Comment, w => w.Buffer.Text).InitializeFromSource();
        }

        private void ConfigureEntries()
        {
            var builder =
                new CommonEEVMBuilderFactory<OrderFrom1c>(ViewModel.ParentTab, ViewModel.Order,
                    UnitOfWorkFactory.CreateWithoutRoot(), MainClass.MainWin.NavigationManager, ViewModel.AutofacScope);

            var viewModel = builder.ForProperty(x => x.Counterparty)
                .UseViewModelJournalAndAutocompleter<CounterpartyJournalViewModel>()
                .Finish();

            counterpartyEntry.ViewModel = viewModel;
            counterpartyEntry.Binding.AddBinding(ViewModel, vm => vm.IsCounterpartySensitive, w => w.Sensitive).InitializeFromSource();

            var deliveryPointBuilder =
                new CommonEEVMBuilderFactory<OrderFrom1c>(ViewModel.ParentTab, ViewModel.Order,
                    UnitOfWorkFactory.CreateWithoutRoot(), MainClass.MainWin.NavigationManager, ViewModel.AutofacScope);

            var deliveryPointViewModel = deliveryPointBuilder.ForProperty(x => x.DeliveryPoint)
                .UseViewModelJournalAndAutocompleter<DeliveryPointJournalViewModel>()
                .Finish();

            deliveryPointEntry.ViewModel = deliveryPointViewModel;
        }
    }
}
