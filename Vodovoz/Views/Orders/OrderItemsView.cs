using System;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Gamma.ColumnConfig;
using Gamma.GtkWidgets.Cells;
using Gtk;
using QS.Tdi;
using QS.Utilities;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
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
            ConfigureTree();
            
            ybtnAddOrderItem.Clicked += (sender, args) => ViewModel.AddSalesItemCommand.Execute();
            ybtnRemoveOrderItem.Clicked += (sender, args) =>
	            ViewModel.RemoveSalesItemCommand.Execute(ytreeViewOrderItems.GetSelectedObjects());
            ychkMovementEquipments.Toggled += YchkMovementEquipmentsOnToggled;
            ychkMovementEquipments.Binding.AddBinding(ViewModel, vm => vm.IsMovementItemsVisible, w => w.Active).InitializeFromSource();
            ychkDeposits.Toggled += YchkDepositsOnToggled;
            ychkDeposits.Binding.AddBinding(ViewModel, vm => vm.IsDepositsReturnsVisible, w => w.Active).InitializeFromSource();
            
            AddOrderMovementItemsView();
            AddOrderDepositReturnsItemsView();
            
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ConfigureTree()
        {
            /*var colorBlack = new Gdk.Color(0, 0, 0);
			var colorBlue = new Gdk.Color(0, 0, 0xff);
			var colorGreen = new Gdk.Color(0, 0xff, 0);
			var colorWhite = new Gdk.Color(0xff, 0xff, 0xff);
			var colorLightYellow = new Gdk.Color(0xe1, 0xd6, 0x70);
			var colorLightRed = new Gdk.Color(0xff, 0x66, 0x66);

			ytreeViewOrderItems.ColumnsConfig = FluentColumnsConfig<OrderSalesItem>.Create()
				.AddColumn("Номенклатура")
					.HeaderAlignment(0.5f)
					.AddTextRenderer(node => node.NomenclatureString)
				.AddColumn(!orderRepository.GetStatusesForActualCount(ViewModel.Order).Contains(ViewModel.Order.Status) ? "Кол-во" : "Кол-во [Факт]")
					.SetTag("Count")
					.HeaderAlignment(0.5f)
					.AddNumericRenderer(node => node.Count)
					.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0))
					.AddSetter((c, node) => 
						c.Digits = node.Nomenclature.Unit == null ? 0 : (uint)node.Nomenclature.Unit.Digits)
					.AddSetter((c, node) => 
						c.Editable = node.CanEditAmount).WidthChars(10)
					.AddTextRenderer(node => 
						node.ActualCount.HasValue ? $"[{node.ActualCount}]" : string.Empty)
					.AddTextRenderer(node => 
						node.CanShowReturnedCount ? string.Format("({0})", node.ReturnedCount) : string.Empty)
					.AddTextRenderer(node => 
						node.Nomenclature.Unit == null ? string.Empty : node.Nomenclature.Unit.Name, false)
				.AddColumn("Аренда")
					.HeaderAlignment(0.5f)
					.AddNumericRenderer(node => node.RentCount).Editing().Digits(0)
					.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0))
					.AddSetter((c, node) => c.Visible = node.RentVisible)
				.AddColumn("Цена")
					.HeaderAlignment(0.5f)
					.AddNumericRenderer(node => node.Price).Digits(2).WidthChars(10)
					.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0)).Editing(true)
					.AddSetter((c, node) => c.Editable = node.CanEditPrice)
					.AddSetter((NodeCellRendererSpin<OrderSalesItem> c, OrderSalesItem node) => {
						if(ViewModel.Order.Status == OrderStatus.NewOrder || 
						   (ViewModel.Order.Status == OrderStatus.WaitForPayment && ViewModel.Order.Type != OrderType.SelfDeliveryOrder))//костыль. на Win10 не видна цветная цена, если виджет засерен
						{
							c.ForegroundGdk = colorBlack;
							var fixedPrice = ViewModel.Order.GetFixedPriceOrNull(node.Nomenclature);
							if(fixedPrice != null) {
								c.ForegroundGdk = colorGreen;
							} else if(node.IsUserPrice && Nomenclature.GetCategoriesWithEditablePrice().Contains(node.Nomenclature.Category)) {
								c.ForegroundGdk = colorBlue;
							}
						}
					})
					.AddTextRenderer(node => CurrencyWorks.CurrencyShortName, false)
				.AddColumn("В т.ч. НДС")
					.HeaderAlignment(0.5f)
					.AddTextRenderer(x => CurrencyWorks.GetShortCurrencyString(x.IncludeNDS ?? 0))
					.AddSetter((c, n) => 
						c.Visible = ViewModel.Order.PaymentType == PaymentType.cashless)
				.AddColumn("Сумма")
					.HeaderAlignment(0.5f)
					.AddTextRenderer(node => CurrencyWorks.GetShortCurrencyString(node.ActualSum))
					.AddSetter((c, n) => {
						if(ViewModel.Order.Status == OrderStatus.DeliveryCanceled
						   || ViewModel.Order.Status == OrderStatus.NotDelivered)
							c.Text = CurrencyWorks.GetShortCurrencyString(n.OriginalSum);
						}
					)
				.AddColumn("Скидка")
					.HeaderAlignment(0.5f)
					.AddNumericRenderer(node => node.ManualChangingDiscount)
					.AddSetter((c, n) => c.Editable = n.PromoSet == null)
					.AddSetter(
						(c, n) => c.Adjustment = n.IsDiscountInMoney
									? new Adjustment(0, 0, (double)(n.Price * n.CurrentCount), 1, 100, 1)
									: new Adjustment(0, 0, 100, 1, 100, 1)
					)
					.AddSetter((c, n) => {
						if(ViewModel.Order.Status == OrderStatus.DeliveryCanceled
						   || ViewModel.Order.Status == OrderStatus.NotDelivered)
							c.Text = n.ManualChangingOriginalDiscount.ToString();
					}) 
					.Digits(2)
					.WidthChars(10)
					.AddTextRenderer(n => n.IsDiscountInMoney ? CurrencyWorks.CurrencyShortName : "%", false)
				.AddColumn("Скидка \nв рублях?")
					.AddToggleRenderer(x => x.IsDiscountInMoney)
					.AddSetter((c, n) => c.Activatable = n.PromoSet == null)
				.AddColumn("Основание скидки")
					.HeaderAlignment(0.5f)
					.AddComboRenderer(node => node.DiscountReason)
					.SetDisplayFunc(x => x.Name)
					.FillItems(orderRepository.GetDiscountReasons(ViewModel.UoW))
					.AddSetter((c, n) => c.Editable = n.Discount > 0 && n.PromoSet == null)
					.AddSetter(
						(c, n) => c.BackgroundGdk = n.Discount > 0 && n.DiscountReason == null
						? colorLightRed
						: colorWhite
					)
					.AddSetter((c, n) => {
						if(ViewModel.Order.Status == OrderStatus.DeliveryCanceled
						   || ViewModel.Order.Status == OrderStatus.NotDelivered)
							c.Text = n.OriginalDiscountReason?.Name ?? n.DiscountReason?.Name;
						}
					)
				.AddColumn("Промо-наборы").SetTag(nameof(ViewModel.Order.PromotionalSets))
					.HeaderAlignment(0.5f)
					.AddTextRenderer(node => node.PromoSet == null ? "" : node.PromoSet.Name)
				.RowCells()
					.XAlign(0.5f)
				.Finish();
			ytreeViewOrderItems.ItemsDataSource = ViewModel.Order.ObservableOrderSalesItems;
			ytreeViewOrderItems.Selection.Changed += TreeItems_Selection_Changed;
			ytreeViewOrderItems.ColumnsConfig.GetColumnsByTag(nameof(ViewModel.Order.PromotionalSets))
				.FirstOrDefault().Visible = ViewModel.Order.PromotionalSets.Count > 0;*/
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
