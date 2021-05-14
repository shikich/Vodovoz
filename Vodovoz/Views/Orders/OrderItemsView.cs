using System;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Gamma.ColumnConfig;
using Gamma.GtkWidgets.Cells;
using Gtk;
using QS.Dialog;
using QS.Project.Dialogs;
using QS.Project.Dialogs.GtkUI;
using QS.Project.Services;
using QS.Tdi;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using QSOrmProject;
using QSWidgetLib;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Repositories;
using Vodovoz.Representations;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.ViewModels.Orders;
using IntToStringConverter = Vodovoz.Infrastructure.Converters.IntToStringConverter;
using Widget = Gtk.Widget;

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
            
            yCmbPromoSets.ItemsList = ViewModel.PromotionalSets;
            ycomboboxReason.SetRenderTextFunc<DiscountReason>(x => x.Name);
            ycomboboxReason.ItemsList = ViewModel.DiscountReasons;
            
            entryBottlesToReturn.ValidationMode = ValidationType.numeric;
            entryBottlesToReturn.Binding.AddBinding(ViewModel, e => e.BottlesReturn, w => w.Text, new IntToStringConverter()).InitializeFromSource();
            
            yhboxReturnTareReason.Binding.AddBinding(ViewModel, vm => vm.IsReturnTareReasonCategoryVisible, w => w.Visible).InitializeFromSource();
            yhboxReasons.Binding.AddBinding(ViewModel, vm => vm.IsReturnTareReasonVisible, w => w.Visible).InitializeFromSource();
            
            yCmbReturnTareReasonCategories.SetRenderTextFunc<ReturnTareReasonCategory>(x => x.Name);
            yCmbReturnTareReasonCategories.ItemsList = ViewModel.ReturnTareReasonCategories;
            yCmbReturnTareReasonCategories.Binding.AddBinding(ViewModel, vm => vm.ReturnTareReasonCategory, w => w.SelectedItem).InitializeFromSource();
            yCmbReturnTareReasonCategories.Changed += (Sender, e) => ViewModel.ChangeReturnTareReasonVisibility();

            yCmbReturnTareReasons.SetRenderTextFunc<ReturnTareReason>(x => x.Name);
            yCmbReturnTareReasons.Binding.AddBinding(ViewModel, vm => vm.ReturnTareReasons, w => w.ItemsList).InitializeFromSource();
            yCmbReturnTareReasons.Binding.AddBinding(ViewModel, vm => vm.ReturnTareReason, w => w.SelectedItem).InitializeFromSource();
            
            enumDiscountUnit.SetEnumItems((DiscountUnits[])Enum.GetValues(typeof(DiscountUnits)));
            enumAddRentButton.ItemsEnum = typeof(RentType);
            enumAddRentButton.EnumItemClicked += (sender, e) => AddRent((RentType)e.ItemEnum);
            
            yspinBtnDiscount.Adjustment.Upper = 100;

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
        
        #region Аренда
		//TODO Возможно все эти методы можно будет перенести в VM, когда перепишутся журналы PermissionControlledRepresentationJournal
		private void AddRent(RentType rentType)
		{
			if (ViewModel.Order.Type == OrderType.VisitingMasterOrder) {
				ViewModel.CommonServices.InteractiveService.ShowMessage(
					ImportanceLevel.Error, 
					"Нельзя добавлять аренду в сервисный заказ", 
					"Ошибка"
				);
				return;
			}
			switch (rentType) {
				case RentType.NonfreeRent:
					SelectPaidRentPackage(RentType.NonfreeRent);
					break;
				case RentType.DailyRent:
					SelectPaidRentPackage(RentType.DailyRent);
					break;
				case RentType.FreeRent:
					SelectFreeRentPackage();
					break;
			}
		}
		
		#region PaidRent
		
		private void SelectPaidRentPackage(RentType rentType)
		{
			var ormReference = new OrmReference(typeof(PaidRentPackage)) {
				Mode = OrmReferenceMode.Select
			};
			ormReference.ObjectSelected += (sender, e) => {
				if (!(e.Subject is PaidRentPackage selectedRentPackage)) {
					return;
				}
				var paidRentPackage = ViewModel.UoW.GetById<PaidRentPackage>(selectedRentPackage.Id);
				SelectEquipmentForPaidRentPackage(rentType, paidRentPackage);
			};
			//TabParent.AddTab(ormReference, this);
		}

		private void SelectEquipmentForPaidRentPackage(RentType rentType, PaidRentPackage paidRentPackage)
		{
			if (ServicesConfig.InteractiveService.Question("Подобрать оборудование автоматически по типу?")) {
				var existingItems = ViewModel.Order.OrderEquipments
					.Where(x => x.OrderRentDepositItem != null || x.OrderRentServiceItem != null)
					.Select(x => x.Nomenclature.Id)
					.Distinct()
					.ToArray();
				
				var anyNomenclature = EquipmentRepositoryForViews.GetAvailableNonSerialEquipmentForRent(ViewModel.UoW, paidRentPackage.EquipmentType, existingItems);
				AddPaidRent(rentType, paidRentPackage, anyNomenclature);
			}
			else {
				var selectDialog = new PermissionControlledRepresentationJournal(new EquipmentsNonSerialForRentVM(ViewModel.UoW, paidRentPackage.EquipmentType));
				selectDialog.Mode = JournalSelectMode.Single;
				selectDialog.CustomTabName("Оборудование для аренды");
				selectDialog.ObjectSelected += (sender, e) => {
					var selectedNode = e.GetNodes<NomenclatureForRentVMNode>().FirstOrDefault();
					if(selectedNode == null) {
						return;
					}
					var nomenclature = ViewModel.UoW.GetById<Nomenclature>(selectedNode.Nomenclature.Id);
					AddPaidRent(rentType, paidRentPackage, nomenclature);
				};
				//TabParent.AddSlaveTab(this, selectDialog);
			}
		}
		
		private void AddPaidRent(RentType rentType, PaidRentPackage paidRentPackage, Nomenclature equipmentNomenclature)
		{
			if (rentType == RentType.FreeRent) {
				throw new InvalidOperationException($"Не правильный тип аренды {RentType.FreeRent}, возможен только {RentType.NonfreeRent} или {RentType.DailyRent}");
			}
			var interactiveService = ServicesConfig.InteractiveService;
			if(equipmentNomenclature == null) {
				interactiveService.ShowMessage(ImportanceLevel.Error, "Для выбранного типа оборудования нет оборудования в справочнике номенклатур.");
				return;
			}

			var stock = StockRepository.GetStockForNomenclature(ViewModel.UoW, equipmentNomenclature.Id);
			if(stock <= 0) {
				if(!interactiveService.Question($"На складах не найдено свободного оборудования\n({equipmentNomenclature.Name})\nДобавить принудительно?")) {
					return;
				}
			}

			switch (rentType) {
				case RentType.NonfreeRent:
					//TODO Реализовать метод, когда будет понятно где он будет находиться
					//Entity.AddNonFreeRent(paidRentPackage, equipmentNomenclature);
					break;
				case RentType.DailyRent:
					//TODO Реализовать метод, когда будет понятно где он будет находиться
					//Entity.AddDailyRent(paidRentPackage, equipmentNomenclature);
					break;
			}
		}

		#endregion PaidRent

		#region FreeRent

		private void SelectFreeRentPackage()
		{
			var ormReference = new OrmReference(typeof(FreeRentPackage)) {
				Mode = OrmReferenceMode.Select
			};
			ormReference.ObjectSelected += (sender, e) => {
				if (!(e.Subject is FreeRentPackage selectedRentPackage)) {
					return;
				}
				var rentPackage = ViewModel.UoW.GetById<FreeRentPackage>(selectedRentPackage.Id);
				SelectEquipmentForFreeRentPackage(rentPackage);
			};
			//TabParent.AddTab(ormReference, this);
		}

		private void SelectEquipmentForFreeRentPackage(FreeRentPackage freeRentPackage)
		{
			if (ServicesConfig.InteractiveService.Question("Подобрать оборудование автоматически по типу?")) {
				var existingItems = ViewModel.Order.OrderEquipments
					.Where(x => x.OrderRentDepositItem != null || x.OrderRentServiceItem != null)
					.Select(x => x.Nomenclature.Id)
					.Distinct()
					.ToArray();
				
				var anyNomenclature = EquipmentRepositoryForViews.GetAvailableNonSerialEquipmentForRent(ViewModel.UoW, freeRentPackage.EquipmentType, existingItems);
				AddFreeRent(freeRentPackage, anyNomenclature);
			}
			else {
				var selectDialog = new PermissionControlledRepresentationJournal(new EquipmentsNonSerialForRentVM(ViewModel.UoW, freeRentPackage.EquipmentType));
				selectDialog.Mode = JournalSelectMode.Single;
				selectDialog.CustomTabName("Оборудование для аренды");
				selectDialog.ObjectSelected += (sender, e) => {
					var selectedNode = e.GetNodes<NomenclatureForRentVMNode>().FirstOrDefault();
					if(selectedNode == null) {
						return;
					}
					var nomenclature = ViewModel.UoW.GetById<Nomenclature>(selectedNode.Nomenclature.Id);
					AddFreeRent(freeRentPackage, nomenclature);
				};
				//TabParent.AddSlaveTab(this, selectDialog);
			}
		}
		
		private void AddFreeRent(FreeRentPackage freeRentPackage, Nomenclature equipmentNomenclature)
		{
			var interactiveService = ServicesConfig.InteractiveService;
			if(equipmentNomenclature == null) {
				interactiveService.ShowMessage(ImportanceLevel.Error, "Для выбранного типа оборудования нет оборудования в справочнике номенклатур.");
				return;
			}

			var stock = StockRepository.GetStockForNomenclature(ViewModel.UoW, equipmentNomenclature.Id);
			if(stock <= 0) {
				if(!interactiveService.Question($"На складах не найдено свободного оборудования\n({equipmentNomenclature.Name})\nДобавить принудительно?")) {
					return;
				}
			}
			
			//TODO Реализовать метод, когда будет понятно где он будет находиться
			//Entity.AddFreeRent(freeRentPackage, equipmentNomenclature);
		}

		#endregion FreeRent

		#endregion

        public override void Destroy()
        {
            ViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
            base.Destroy();
        }
    }
}
