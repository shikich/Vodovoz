using System;
using Gamma.Utilities;
using Gtk;
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
            ConfigureCounterpartyEntry();

            //ylblOrderAuthor.Binding.AddBinding(ViewModel.Order, o => o.Author.ShortName, w => w.LabelProp).InitializeFromSource();

            ylblStatusInfo.Binding.AddFuncBinding(ViewModel.Order, o => o.Status.GetEnumTitle(), w => w.LabelProp).InitializeFromSource();
            ylblCreationDate.Binding.AddFuncBinding(ViewModel.Order, o => o.CreateDate.HasValue ? o.CreateDate.Value.ToString("dd.MM.yyyy HH:mm") : "", w => w.LabelProp).InitializeFromSource();
            ylblBillDate.Binding.AddBinding(ViewModel, vm => vm.IsBillDateVisible, w => w.Visible).InitializeFromSource();
            ylblDeliveryTime.Binding.AddBinding(ViewModel, vm => vm.IsDeliveryScheduleVisible, w => w.Visible).InitializeFromSource();

            datePickerDeliveryDate.Binding.AddBinding(ViewModel.Order, o => o.DeliveryDate, w => w.DateOrNull).InitializeFromSource();
            datePickerDeliveryDate.DateChanged += DatePickerDeliveryDateOnDateChanged;
            datePickerDeliveryDate.DateChangedByUser += ViewModel.PickerDeliveryDateDateOnChangedByUser;

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

            ViewModel.UpdatePaymentTypeListForNaturalCounterparty += UpdatePaymentTypeListForNaturalCounterparty;
            ViewModel.UpdatePaymentTypeListForStopDelivery += UpdatePaymentTypeListForStopDelivery;
            ViewModel.UpdateSelectedPaymentType += UpdateSelectedPaymentType;
        }

        private void DatePickerDeliveryDateOnDateChanged(object sender, EventArgs e)
        {
            //TODO обработать заполнение основания подписи доков
            //ViewModel.Order.SetProxyForOrder();
            UpdateProxyInfo();

            if (datePickerDeliveryDate.Date < DateTime.Today
                && !ViewModel.CommonServices.CurrentPermissionService.ValidatePresetPermission(
                    "can_can_create_order_in_advance"))
            {
                datePickerDeliveryDate.ModifyBase(StateType.Normal, new Gdk.Color(255, 0, 0));
            }
            else
            {
                datePickerDeliveryDate.ModifyBase(StateType.Normal, new Gdk.Color(255, 255, 255));
            }
        }

        protected void OnEnumPaymentTypeChanged(object sender, EventArgs e)
        {
            //при изменении типа платежа вкл/откл кнопку "ожидание оплаты"
            //TODO Перенести в OrderItemsViewModel
            //buttonWaitForPayment.Sensitive = IsPaymentTypeBarterOrCashless();

            //TODO узнать нужно это поле в новых заказах или нет
            /*enumSignatureType.Visible = labelSignatureType.Visible =
                (Entity.Client != null &&
                    (Entity.Client.PersonType == PersonType.legal || Entity.PaymentType == PaymentType.cashless)
                );
            */
            //TODO Перенести в OrderItemsViewModel
            //if(treeItems.Columns.Any())
            //    treeItems.Columns.First(x => x.Title == "В т.ч. НДС").Visible = Entity.PaymentType == PaymentType.cashless;

            //TODO Перенести в OrderItemsViewModel если эти поля будут в заказе
            //spinSumDifference.Visible = labelSumDifference.Visible = labelSumDifferenceReason.Visible =
            //    dataSumDifferenceReason.Visible = (Entity.PaymentType == PaymentType.cash || Entity.PaymentType == PaymentType.BeveragesWorld);
            //spinSumDifference.Visible = spinSumDifference.Visible && ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_edit_order_extra_cash");
            //Entity.SetProxyForOrder();
            //UpdateProxyInfo();
            //UpdateUIState();
        }

        void UpdateProxyInfo()
        {
            /*
            bool canShow = ViewModel.Order.Counterparty != null 
                           && ViewModel.Order.DeliveryDate.HasValue 
                           && (ViewModel.Order.Counterparty?.PersonType == PersonType.legal 
                               || ViewModel.Order.PaymentType == PaymentType.cashless);

            labelProxyInfo.Visible = canShow;

            DBWorks.SQLHelper text = new DBWorks.SQLHelper("");
            if(canShow) {
                var proxies = ViewModel.Order.Counterparty.Proxies.Where(
                    p => p.IsActiveProxy(ViewModel.Order.DeliveryDate.Value) 
                         && (p.DeliveryPoints == null 
                             || !p.DeliveryPoints.Any() 
                             || p.DeliveryPoints.Any(x => DomainHelper.EqualDomainObjects(x, ViewModel.Order.DeliveryPoint))));
                
                foreach(var proxy in proxies) {
                    if(!string.IsNullOrWhiteSpace(text.Text))
                        text.Add("\n");
                    text.Add(string.Format("Доверенность{2} №{0} от {1:d}", proxy.Number, proxy.IssueDate,
                        proxy.DeliveryPoints == null ? "(общая)" : ""));
                    text.StartNewList(": ");
                    foreach(var pers in proxy.Persons) {
                        text.AddAsList(pers.NameWithInitials);
                    }
                }
            }
            if(string.IsNullOrWhiteSpace(text.Text))
                labelProxyInfo.Markup = "<span foreground=\"red\">Нет активной доверенности</span>";
            else
                labelProxyInfo.LabelProp = text.Text;
                */
        }

        private void ConfigureCounterpartyEntry()
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

        private void UpdatePaymentTypeListForNaturalCounterparty(bool isClearHideList)
        {
            if (!isClearHideList)
            {
                enumPaymentType.AddEnumToHideList(ViewModel.HidePaymentTypesForNaturalCounterparty);
            }
            else
            {
                enumPaymentType.RemoveEnumFromHideList(ViewModel.HidePaymentTypesForNaturalCounterparty);
            }
        }

        private void UpdatePaymentTypeListForStopDelivery(object[] hidedList)
        {
            enumPaymentType.AddEnumToHideList(hidedList);
        }

        private void UpdateSelectedPaymentType(PaymentType paymentType) => enumPaymentType.SelectedItem = paymentType;
    }
}
