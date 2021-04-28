using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using QS.BusinessCommon.Domain;
using QS.Commands;
using QS.Dialog;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
using Vodovoz.Factories;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.ViewModels.Dialogs.Orders;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderItemsViewModel : UoWWidgetViewModelBase
    {
        private bool isMovementItemsVisible;
        public OrderBase Order { get; set; }
        
        public ILifetimeScope AutofacScope { get; set; }

        public bool IsMovementItemsVisible
        {
            get => isMovementItemsVisible;
            set => SetField(ref isMovementItemsVisible, value);
        }
        
        private bool isDepositsReturnsVisible;
        public bool IsDepositsReturnsVisible
        {
            get => isDepositsReturnsVisible;
            set => SetField(ref isDepositsReturnsVisible, value);
        }
        
        private NomenclaturesJournalViewModel NomenclaturesForSaleJournalViewModel { get; set; }

        private NomenclaturesJournalViewModel activeNomenclatureJournalViewModel;
        public NomenclaturesJournalViewModel ActiveNomenclatureJournalViewModel
        {
            get => activeNomenclatureJournalViewModel;
            set => SetField(ref activeNomenclatureJournalViewModel, value);
        }
        
        private OrderDepositReturnsItemsViewModel orderDepositReturnsItemsViewModel;
        public OrderDepositReturnsItemsViewModel OrderDepositReturnsItemsViewModel
        {
            get
            {
                if (orderDepositReturnsItemsViewModel == null)
                {
                    Parameter[] parameters =
                    {
                        new TypedParameter(typeof(NomenclatureFilterViewModel),
                            AutofacScope.Resolve<INomenclatureFilterViewModelFactory>()),
                        new TypedParameter(typeof(INomenclaturesJournalViewModelFactory),
                            nomenclaturesJournalViewModelFactory),
                        new TypedParameter(typeof(OrderBase), Order)
                    };
                    orderDepositReturnsItemsViewModel = 
                        AutofacScope.Resolve<OrderDepositReturnsItemsViewModel>(parameters);
                }

                return orderDepositReturnsItemsViewModel;
            }
        }
        
        private OrderMovementItemsViewModel orderMovementItemsViewModel;
        public OrderMovementItemsViewModel OrderMovementItemsViewModel
        {
            get
            {
                if (orderMovementItemsViewModel == null)
                {
                    Parameter[] parameters =
                    {
                        new TypedParameter(typeof(IInteractiveService), interactiveService),
                        new TypedParameter(typeof(NomenclatureFilterViewModel),
                            AutofacScope.Resolve<INomenclatureFilterViewModelFactory>()),
                        new TypedParameter(typeof(INomenclaturesJournalViewModelFactory),
                            nomenclaturesJournalViewModelFactory),
                        new TypedParameter(typeof(OrderBase), Order)
                    };
                    orderMovementItemsViewModel = AutofacScope.Resolve<OrderMovementItemsViewModel>(parameters);
                }

                return orderMovementItemsViewModel;
            }
        }

        #region Команды

        private DelegateCommand addSalesItemCommand;
        public DelegateCommand AddSalesItemCommand => addSalesItemCommand ?? (
            addSalesItemCommand = new DelegateCommand(
                () =>
                {
                    if(!CanAddNomenclaturesToOrder())
                        return;
                    
                    OrderMovementItemsViewModelOnRemoveActiveViewModel();
                    
                    NomenclaturesForSaleJournalViewModel?.UpdateOnChanges(
                        typeof(Nomenclature),
                        typeof(MeasurementUnits),
                        typeof(WarehouseMovementOperation),
                        typeof(Order),
                        typeof(OrderItem)
                    );
                    
                    if (NomenclaturesForSaleJournalViewModel == null)
                    {
                        var nomenclatureFilter = 
                            AutofacScope.Resolve<INomenclatureFilterViewModelFactory>()
                                        .CreateNomenclatureForSaleFilter(GetDefaultNomenclatureCategory());
                        
                        var journalViewModel =
                            nomenclaturesJournalViewModelFactory.CreateNomenclaturesJournalViewModel(nomenclatureFilter);
                        journalViewModel.AdditionalJournalRestriction =
                            new NomenclaturesForOrderJournalRestriction(ServicesConfig.CommonServices);
                        journalViewModel.TabName = "Номенклатура на продажу";
                        journalViewModel.OnEntitySelectedResultWithoutClose += (s, ea) =>
                        {
                            var selectedNode = ea.SelectedNodes.FirstOrDefault();
                            
                            if (selectedNode == null)
                                return;
                            
                            //TryAddNomenclature(UoW.Session.Get<Nomenclature>(selectedNode.Id));
                            OrderMovementItemsViewModelOnRemoveActiveViewModel();
                        };

                        NomenclaturesForSaleJournalViewModel = journalViewModel;
                    }
                    ActiveNomenclatureJournalViewModel = NomenclaturesForSaleJournalViewModel;
                },
                () => true
            )
        );

        private DelegateCommand<object[]> removeSalesItemCommand;
        public DelegateCommand<object[]> RemoveSalesItemCommand => 
            removeSalesItemCommand ?? (removeSalesItemCommand = new DelegateCommand<object[]>(
                items =>
                {
                    
                },
                items => items.Any()
            )
        );

        #endregion Commands
        
        private readonly IInteractiveService interactiveService;
        private readonly ICurrentUserSettings currentUserSettings;
        private readonly INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory;
        public OrderInfoExpandedPanelViewModel OrderInfoExpandedPanelViewModel { get; }
        
        public OrderItemsViewModel(
            IInteractiveService interactiveService,
            ICurrentUserSettings currentUserSettings,
            OrderBase order,
            OrderInfoExpandedPanelViewModel orderInfoExpandedPanelViewModel,
            ILifetimeScope scope,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory)
        {
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            this.currentUserSettings = currentUserSettings ?? throw new ArgumentNullException(nameof(currentUserSettings));
            this.nomenclaturesJournalViewModelFactory = 
                nomenclaturesJournalViewModelFactory ??
                    throw new ArgumentNullException(nameof(nomenclaturesJournalViewModelFactory));
            Order = order;
            OrderInfoExpandedPanelViewModel = orderInfoExpandedPanelViewModel;
            AutofacScope = scope;
            
            OrderMovementItemsViewModel.UpdateActiveViewModel += OrderMovementItemsViewModelOnUpdateActiveViewModel;
            OrderMovementItemsViewModel.RemoveActiveViewModel += OrderMovementItemsViewModelOnRemoveActiveViewModel;
            OrderDepositReturnsItemsViewModel.UpdateActiveViewModel += OrderMovementItemsViewModelOnUpdateActiveViewModel;
            OrderDepositReturnsItemsViewModel.RemoveActiveViewModel += OrderMovementItemsViewModelOnRemoveActiveViewModel;
        }

        private void OrderMovementItemsViewModelOnRemoveActiveViewModel()
        {
            ActiveNomenclatureJournalViewModel?.Items.Clear();
            ActiveNomenclatureJournalViewModel = null;
        }

        private void OrderMovementItemsViewModelOnUpdateActiveViewModel(NomenclaturesJournalViewModel journalViewModel)
        {
            if (ActiveNomenclatureJournalViewModel != journalViewModel) {
                OrderMovementItemsViewModelOnRemoveActiveViewModel();
                ActiveNomenclatureJournalViewModel = journalViewModel;
            }
        }

        bool CanAddNomenclaturesToOrder()
        {
            if(Order.Counterparty == null) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Warning, 
                    "Для добавления товара на продажу должен быть выбран клиент.");
                
                return false;
            }

            if(Order.DeliveryPoint == null && Order.Type != OrderType.SelfDeliveryOrder) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Warning,
                    "Для добавления товара на продажу должна быть выбрана точка доставки.");
                
                return false;
            }

            return true;
        }
        
        private void TryAddNomenclature(
            Nomenclature nomenclature, 
            decimal count = 0, 
            decimal discount = 0, 
            DiscountReason discountReason = null)
        {
            if(Order is OrderFrom1c)
                return;

            if(Order.OrderSalesItems.Any(x => !Nomenclature.GetCategoriesForMaster().Contains(x.Nomenclature.Category))
                && nomenclature.Category == NomenclatureCategory.master) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Info, "В не сервисный заказ нельзя добавить сервисную услугу");
                return;
            }

            if(Order.OrderSalesItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.master)
                && !Nomenclature.GetCategoriesForMaster().Contains(nomenclature.Category)) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Info, "В сервисный заказ нельзя добавить не сервисную услугу");
                return;
            }
            
            if(nomenclature.OnlineStore != null && !ServicesConfig.CommonServices.CurrentPermissionService
                .ValidatePresetPermission("can_add_online_store_nomenclatures_to_order")) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Info,
                    "У вас недостаточно прав для добавления на продажу номенклатуры интернет магазина");
                return;
            }
			
            AddNomenclature(nomenclature, count, discount, false, discountReason);
        }
        
        public virtual void AddNomenclature(
            Nomenclature nomenclature, 
            decimal count = 0, 
            decimal discount = 0, 
            bool discountInMoney = false, 
            DiscountReason discountReason = null, 
            PromotionalSet proSet = null)
        {
            /*
            switch(nomenclature.Category) {
                case NomenclatureCategory.water:
                    AddWaterForSale(nomenclature, count, discount, discountInMoney, discountReason, proSet);
                    break;
                case NomenclatureCategory.master:
                    Order.Contract = CreateServiceContractAddMasterNomenclature(nomenclature);
                    break;
                default:
                    var orderSalesItem = new OrderSalesItem {
                        Count = count,
                        Nomenclature = nomenclature,
                        Price = nomenclature.GetPrice(1),
                        IsDiscountInMoney = discountInMoney,
                        // TODO Поправить утсановку свойства
                        //DiscountSetter = discount,
                        DiscountReason = discountReason,
                        PromoSet = proSet
                    };
                    AddItemWithNomenclatureForSale(orderSalesItem);
                    break;
            }*/
        }

        private NomenclatureCategory GetDefaultNomenclatureCategory()
        {
            var defaultCategory = NomenclatureCategory.water;
            
            if(currentUserSettings.GetUserSettings().DefaultSaleCategory.HasValue) {
                defaultCategory = currentUserSettings.GetUserSettings().DefaultSaleCategory.Value;
            }

            return defaultCategory;
        }
    }
}
