using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Autofac.Core;
using QS.BusinessCommon.Domain;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.EntityRepositories.Nodes;
using Vodovoz.Factories;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.Journals.Nodes.Rent;
using Vodovoz.Repositories;
using Vodovoz.ViewModels.Dialogs.Orders;
using Vodovoz.ViewModels.Journals.JournalNodes.Nomenclatures;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;
using Vodovoz.ViewModels.Journals.JournalViewModels.Rent;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderItemsViewModel : UoWWidgetViewModelBase
    {
        private const string NomenclatureForSaleJournalName = "Номенклатура на продажу";
        
        private bool isMovementItemsVisible;
        
        public OrderBase Order { get; set; }
        public ILifetimeScope AutofacScope { get; set; }
        public IEnumerable<PromotionalSet> PromotionalSets { get; private set; }
        public IEnumerable<ReturnTareReasonCategory> ReturnTareReasonCategories { get; private set; }
        public IEnumerable<DiscountReason> DiscountReasons { get; private set; }

        public bool IsMovementItemsVisible
        {
            get => isMovementItemsVisible;
            set => SetField(ref isMovementItemsVisible, value);
        }
        
        private IEnumerable<ReturnTareReason> returnTareReasons;
        public IEnumerable<ReturnTareReason> ReturnTareReasons
        {
            get => returnTareReasons;
            private set => SetField(ref returnTareReasons, value);
        }
        
        private ReturnTareReasonCategory returnTareReasonCategory;
        public ReturnTareReasonCategory ReturnTareReasonCategory
        {
            get => returnTareReasonCategory;
            set => SetField(ref returnTareReasonCategory, value);
        }
        
        private ReturnTareReason returnTareReason;
        public ReturnTareReason ReturnTareReason
        {
            get => returnTareReason;
            set => SetField(ref returnTareReason, value);
        }
        
        int? bottlesReturn;
        public int? BottlesReturn {
            get => bottlesReturn;
            set
            {
                if(SetField(ref bottlesReturn, value))
                {
                    ReturnTareReasonCategoryShow();
                }
            }
        }
        
        private bool isReturnTareReasonVisible;
        public bool IsReturnTareReasonVisible
        {
            get => isReturnTareReasonVisible;
            set => SetField(ref isReturnTareReasonVisible, value);
        }
        
        private bool isReturnTareReasonCategoryVisible;
        public bool IsReturnTareReasonCategoryVisible
        {
            get => isReturnTareReasonCategoryVisible;
            set => SetField(ref isReturnTareReasonCategoryVisible, value);
        }
        
        private bool isDepositsReturnsVisible;
        public bool IsDepositsReturnsVisible
        {
            get => isDepositsReturnsVisible;
            set => SetField(ref isDepositsReturnsVisible, value);
        }
        
        private bool isActionsForSaleVisible;
        public bool IsActionsForSaleVisible
        {
            get => isActionsForSaleVisible;
            set => SetField(ref isActionsForSaleVisible, value);
        }

        private bool isDiscountVisible;
        public bool IsDiscountVisible
        {
            get => isDiscountVisible;
            set => SetField(ref isDiscountVisible, value);
        }
        
        private bool canEditMovementItems;
        public bool CanEditMovementItems
        {
            get => canEditMovementItems;
            set => SetField(ref canEditMovementItems, value);
        }
        
        private bool canEditDepositItems;
        public bool CanEditDepositItems
        {
            get => canEditDepositItems;
            set => SetField(ref canEditDepositItems, value);
        }
        
        private NomenclaturesJournalViewModel NomenclaturesForSaleJournalViewModel { get; set; }
        private PaidRentPackagesJournalViewModel PaidRentPackagesJournalViewModel { get; set; }
        private FreeRentPackagesJournalViewModel FreeRentPackagesJournalViewModel { get; set; }
        private NonSerialEquipmentsForRentJournalViewModel NonSerialEquipmentsForRentJournalViewModel { get; set; }

        private JournalViewModelBase activeJournalViewModel;
        public JournalViewModelBase ActiveJournalViewModel
        {
            get => activeJournalViewModel;
            set => SetField(ref activeJournalViewModel, value);
        }
        
        private OrderDepositReturnsItemsViewModel orderDepositReturnsItemsViewModel;
        public OrderDepositReturnsItemsViewModel OrderDepositReturnsItemsViewModel
        {
            get
            {
                if(orderDepositReturnsItemsViewModel == null)
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
                if(orderMovementItemsViewModel == null)
                {
                    Parameter[] parameters =
                    {
                        new TypedParameter(typeof(IInteractiveService), commonServices.InteractiveService),
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
                    
                    if(NomenclaturesForSaleJournalViewModel == null)
                    {
                        var nomenclatureFilter = 
                            AutofacScope.Resolve<INomenclatureFilterViewModelFactory>()
                                        .CreateNomenclatureForSaleFilter(GetDefaultNomenclatureCategory());
                        
                        var journalViewModel =
                            nomenclaturesJournalViewModelFactory.CreateNomenclaturesJournalViewModel(nomenclatureFilter);
                        journalViewModel.AdditionalJournalRestriction =
                            new NomenclaturesForOrderJournalRestriction(commonServices);
                        journalViewModel.TabName = NomenclatureForSaleJournalName;
                        journalViewModel.OnSelectResultWithoutClose += (s, ea) =>
                        {
                            var selectedNode = ea.SelectedObjects.OfType<NomenclatureJournalNode>().FirstOrDefault();
                            
                            if (selectedNode == null)
                                return;
                            
                            //TryAddNomenclature(UoW.Session.Get<Nomenclature>(selectedNode.Id));
                        };

                        NomenclaturesForSaleJournalViewModel = journalViewModel;
                    }

                    if (ActiveJournalViewModel != null 
                        && ActiveJournalViewModel.TabName == NomenclatureForSaleJournalName) {
                        UpdateActiveJournalViewModel(null);
                    }
                    else {
                        NomenclaturesForSaleJournalViewModel?.UpdateOnChanges(
                            typeof(Nomenclature),
                            typeof(MeasurementUnits),
                            typeof(WarehouseMovementOperation),
                            typeof(Order),
                            typeof(OrderItem)
                        );
                        UpdateActiveJournalViewModel(NomenclaturesForSaleJournalViewModel);
                        OrderMovementItemsViewModel.DeactivateActiveJournalViewModels();
                    }
                    
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
        
        private DelegateCommand<RentType> addRentCommand;
        public DelegateCommand<RentType> AddRentCommand => 
            addRentCommand ?? (addRentCommand = new DelegateCommand<RentType>(
                    rentType =>
                    {
                        if(Order.Type == OrderType.VisitingMasterOrder) {
                            commonServices.InteractiveService.ShowMessage(
                                ImportanceLevel.Error, 
                                "Нельзя добавлять аренду в сервисный заказ", 
                                "Ошибка"
                            );
                            return;
                        }
                        
                        switch(rentType) {
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
                    },
                    rentType => true
                )
            );

        #endregion Команды
        
        private readonly ICommonServices commonServices;
        private readonly ICurrentUserSettings currentUserSettings;
        private readonly INomenclatureRepository nomenclatureRepository;
        private readonly INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory;
        private readonly IRentPackagesJournalsViewModelsFactory rentPackagesJournalsViewModelsFactory;
        private readonly INonSerialEquipmentsForRentJournalViewModelFactory nonSerialEquipmentsForRentJournalViewModelFactory;
        public OrderInfoExpandedPanelViewModel OrderInfoExpandedPanelViewModel { get; }
        
        public OrderItemsViewModel(
            ICommonServices commonServices,
            ICurrentUserSettings currentUserSettings,
            INomenclatureRepository nomenclatureRepository,
            OrderBase order,
            OrderInfoExpandedPanelViewModel orderInfoExpandedPanelViewModel,
            ILifetimeScope scope,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory,
            IRentPackagesJournalsViewModelsFactory rentPackagesJournalsViewModelsFactory,
            INonSerialEquipmentsForRentJournalViewModelFactory nonSerialEquipmentsForRentJournalViewModelFactory)
        {
            this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
            this.currentUserSettings = currentUserSettings ?? throw new ArgumentNullException(nameof(currentUserSettings));
            this.nomenclatureRepository = nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
            this.nomenclaturesJournalViewModelFactory = nomenclaturesJournalViewModelFactory ??
                                                        throw new ArgumentNullException(nameof(nomenclaturesJournalViewModelFactory));
            this.rentPackagesJournalsViewModelsFactory = rentPackagesJournalsViewModelsFactory ??
                                                         throw new ArgumentNullException(nameof(rentPackagesJournalsViewModelsFactory));
            this.nonSerialEquipmentsForRentJournalViewModelFactory =
                nonSerialEquipmentsForRentJournalViewModelFactory ??
                    throw new ArgumentNullException(nameof(nonSerialEquipmentsForRentJournalViewModelFactory));
            
            Order = order;
            OrderInfoExpandedPanelViewModel = orderInfoExpandedPanelViewModel;
            AutofacScope = scope;
            //TODO потом убрать, когда определимся с прокидыванием uow
            UoW = UnitOfWorkFactory.CreateWithoutRoot();

            FillLists();
            Order.PropertyChanged += OrderOnPropertyChanged;
            OrderMovementItemsViewModel.UpdateActiveViewModel += UpdateActiveJournalViewModel;
            OrderDepositReturnsItemsViewModel.UpdateActiveViewModel += UpdateActiveJournalViewModel;

            UpdateState();
        }

        #region Аренда

        #region PaidRent

        private void SelectPaidRentPackage(RentType rentType)
        {
            if(PaidRentPackagesJournalViewModel == null)
            {
                var paidRentJournal = rentPackagesJournalsViewModelsFactory.CreatePaidRentPackagesJournalViewModel(
                    false, false, false, false);
                paidRentJournal.OnSelectResult += (sender, args) =>
                {
                    var selectedRentPackage = args.SelectedObjects.OfType<PaidRentPackagesJournalNode>().FirstOrDefault();

                    if (selectedRentPackage == null) return;
                    
                    var paidRentPackage = UoW.GetById<PaidRentPackage>(selectedRentPackage.Id);
                    SelectEquipmentForPaidRentPackage(rentType, paidRentPackage);
                };
                PaidRentPackagesJournalViewModel = paidRentJournal;
            }

            if(ActiveJournalViewModel is PaidRentPackagesJournalViewModel)
            {
                UpdateActiveJournalViewModel(null);
            }
            else
            {
                UpdateActiveJournalViewModel(PaidRentPackagesJournalViewModel);
            }
        }
        
        private void SelectEquipmentForPaidRentPackage(RentType rentType, PaidRentPackage paidRentPackage)
        {
            if(commonServices.InteractiveService.Question("Подобрать оборудование автоматически по типу?")) 
            {
                var existingItems = GetExistingItems();
                var anyNomenclature = 
                    nomenclatureRepository.GetAvailableNonSerialEquipmentForRent(UoW, paidRentPackage.EquipmentType, existingItems);
                AddPaidRent(rentType, paidRentPackage, anyNomenclature);
            }
            else
            {
                if(NonSerialEquipmentsForRentJournalViewModel == null)
                {
                    var nonSerialEquipmentsForRentJournal =
                        nonSerialEquipmentsForRentJournalViewModelFactory.CreateNonSerialEquipmentsForRentJournalViewModel(
                            paidRentPackage.EquipmentType);

                    nonSerialEquipmentsForRentJournal.OnSelectResult += (sender, args) =>
                    {
                        var selectedNode = args.SelectedObjects.OfType<NomenclatureForRentNode>().FirstOrDefault();

                        if (selectedNode == null) return;

                        var nomenclature = UoW.GetById<Nomenclature>(selectedNode.Id);
                        AddPaidRent(rentType, paidRentPackage, nomenclature);
                    };
                    NonSerialEquipmentsForRentJournalViewModel = nonSerialEquipmentsForRentJournal;
                }
                UpdateActiveJournalViewModel(NonSerialEquipmentsForRentJournalViewModel);
            }
        }

        private void AddPaidRent(RentType rentType, PaidRentPackage paidRentPackage, Nomenclature equipmentNomenclature)
        {
            if(rentType == RentType.FreeRent) 
            {
                throw new InvalidOperationException(
                    $"Неправильный тип аренды {RentType.FreeRent}, возможен только {RentType.NonfreeRent} или {RentType.DailyRent}");
            }
            
            if(equipmentNomenclature == null) 
            {
                commonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Error, "Для выбранного типа оборудования нет оборудования в справочнике номенклатур.");
                return;
            }

            var stock = StockRepository.GetStockForNomenclature(UoW, equipmentNomenclature.Id);
            
            if(stock <= 0) 
            {
                if(!commonServices.InteractiveService.Question(
                    $"На складах не найдено свободного оборудования\n({equipmentNomenclature.Name})\nДобавить принудительно?"))
                {
                    //TODO Скорее всего надо закрывать журнал
                    return;
                }
            }

            switch (rentType) 
            {
                case RentType.NonfreeRent:
                    //TODO Реализовать метод, когда будет понятно где он будет находиться
                    //Entity.AddNonFreeRent(paidRentPackage, equipmentNomenclature);
                    break;
                case RentType.DailyRent:
                    //TODO Реализовать метод, когда будет понятно где он будет находиться
                    //Entity.AddDailyRent(paidRentPackage, equipmentNomenclature);
                    break;
            }
            
            UpdateActiveJournalViewModel(null);
        }

        #endregion
        
        #region FreeRent

		private void SelectFreeRentPackage()
		{
            if(FreeRentPackagesJournalViewModel == null)
            {
                var freeRentJournal = rentPackagesJournalsViewModelsFactory.CreateFreeRentPackagesJournalViewModel(
                    false, false, false, false);
                freeRentJournal.OnSelectResult += (sender, args) =>
                {
                    var selectedRentPackage = args.SelectedObjects.OfType<FreeRentPackagesJournalNode>().FirstOrDefault();

                    if(selectedRentPackage == null) return;
                    
                    var freeRentPackage = UoW.GetById<FreeRentPackage>(selectedRentPackage.Id);
                    SelectEquipmentForFreeRentPackage(freeRentPackage);
                };
                FreeRentPackagesJournalViewModel = freeRentJournal;
            }

            if(ActiveJournalViewModel is FreeRentPackagesJournalViewModel)
            {
                UpdateActiveJournalViewModel(null);
            }
            else
            {
                UpdateActiveJournalViewModel(FreeRentPackagesJournalViewModel);
            }
		}

		private void SelectEquipmentForFreeRentPackage(FreeRentPackage freeRentPackage)
		{
			if(commonServices.InteractiveService.Question("Подобрать оборудование автоматически по типу?")) {
				var existingItems = GetExistingItems();
				
				var anyNomenclature = 
                    nomenclatureRepository.GetAvailableNonSerialEquipmentForRent(UoW, freeRentPackage.EquipmentType, existingItems);
				AddFreeRent(freeRentPackage, anyNomenclature);
			}
			else {
                if(NonSerialEquipmentsForRentJournalViewModel == null)
                {
                    var nonSerialEquipmentsForRentJournal =
                        nonSerialEquipmentsForRentJournalViewModelFactory.CreateNonSerialEquipmentsForRentJournalViewModel(
                            freeRentPackage.EquipmentType);

                    nonSerialEquipmentsForRentJournal.OnSelectResult += (sender, args) =>
                    {
                        var selectedNode = args.SelectedObjects.OfType<NomenclatureForRentNode>().FirstOrDefault();

                        if (selectedNode == null) return;

                        var nomenclature = UoW.GetById<Nomenclature>(selectedNode.Id);
                        AddFreeRent(freeRentPackage, nomenclature);
                    };
                    NonSerialEquipmentsForRentJournalViewModel = nonSerialEquipmentsForRentJournal;
                }
                UpdateActiveJournalViewModel(NonSerialEquipmentsForRentJournalViewModel);
			}
		}
		
		private void AddFreeRent(FreeRentPackage freeRentPackage, Nomenclature equipmentNomenclature)
		{
			if(equipmentNomenclature == null) 
            {
				commonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Error, "Для выбранного типа оборудования нет оборудования в справочнике номенклатур.");
				return;
			}

			var stock = StockRepository.GetStockForNomenclature(UoW, equipmentNomenclature.Id);
			if(stock <= 0) {
				if(!commonServices.InteractiveService.Question(
                    $"На складах не найдено свободного оборудования\n({equipmentNomenclature.Name})\nДобавить принудительно?"))
                {
                    //TODO Скорее всего надо закрывать журнал
					return;
				}
			}
			
			//TODO Реализовать метод, когда будет понятно где он будет находиться
			//Entity.AddFreeRent(freeRentPackage, equipmentNomenclature);
            UpdateActiveJournalViewModel(null);
		}

		#endregion FreeRent

        private IEnumerable<int> GetExistingItems()
        {
            return Order.OrderEquipments
                .Where(x => x.OrderRentDepositItem != null || x.OrderRentServiceItem != null)
                .Select(x => x.Nomenclature.Id)
                .Distinct();
        }
        
        #endregion

        private void FillLists()
        {
            ReturnTareReasonCategories = UoW.Session.QueryOver<ReturnTareReasonCategory>().List();
            DiscountReasons = UoW.Session.QueryOver<DiscountReason>().List();
        }

        private void OrderOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Order.Counterparty):
                    UpdateParamsDependOnCounterparty();
                    break;
            }
        }

        private void UpdateParamsDependOnCounterparty()
        {
            var promoSets = UoW.Session.QueryOver<PromotionalSet>().Where(s => !s.IsArchive).List();
            //TODO Заменить создание uow на нормальный
            PromotionalSets = promoSets.Where(s => s.IsValidForOrder(Order, UnitOfWorkFactory.CreateWithoutRoot()));
        }

        private void OrderMovementItemsViewModelOnRemoveActiveViewModel()
        {
            ActiveJournalViewModel?.Items.Clear();
            ActiveJournalViewModel = null;
        }

        private void UpdateActiveJournalViewModel(JournalViewModelBase journalViewModel)
        {
            if(journalViewModel == null)
            {
                OrderMovementItemsViewModelOnRemoveActiveViewModel();
                return;
            }
            
            if(ActiveJournalViewModel != journalViewModel) 
            {
                OrderMovementItemsViewModelOnRemoveActiveViewModel();
                ActiveJournalViewModel = journalViewModel;
            }
        }

        private bool CanAddNomenclaturesToOrder()
        {
            if(Order.Counterparty == null) 
            {
                commonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Warning, 
                    "Для добавления товара на продажу должен быть выбран клиент.");
                
                return false;
            }

            if(Order.DeliveryPoint == null && Order.Type != OrderType.SelfDeliveryOrder) 
            {
                commonServices.InteractiveService.ShowMessage(
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
            {
                return;
            }

            if(Order.OrderSalesItems.Any(x => !Nomenclature.GetCategoriesForMaster().Contains(x.Nomenclature.Category))
                && nomenclature.Category == NomenclatureCategory.master) {
                commonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Info, "В не сервисный заказ нельзя добавить сервисную услугу");
                return;
            }

            if(Order.OrderSalesItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.master)
                && !Nomenclature.GetCategoriesForMaster().Contains(nomenclature.Category)) {
                commonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Info, "В сервисный заказ нельзя добавить не сервисную услугу");
                return;
            }
            
            if(nomenclature.OnlineStore != null && !ServicesConfig.CommonServices.CurrentPermissionService
                .ValidatePresetPermission("can_add_online_store_nomenclatures_to_order")) {
                commonServices.InteractiveService.ShowMessage(
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

        private void RemoveReturnTareReason()
        {
            if (ReturnTareReason != null)
                ReturnTareReason = null;

            if(ReturnTareReasonCategory != null)
                ReturnTareReasonCategory = null;
        }

        public void ChangeReturnTareReasonVisibility()
        {
            if (ReturnTareReasonCategory != null)
            {
                if (!IsReturnTareReasonVisible)
                    IsReturnTareReasonVisible = true;

                ReturnTareReasons = ReturnTareReasonCategory.ChildReasons;
            }
        }
        
        private void ReturnTareReasonCategoryShow()
        {
            if (BottlesReturn.HasValue && BottlesReturn > 0)
            {
                IsReturnTareReasonCategoryVisible = Order.GetTotalWater19LCount() == 0;

                if(!IsReturnTareReasonCategoryVisible) {
                    IsReturnTareReasonVisible = false;
                    RemoveReturnTareReason();
                }
                else {
                    ChangeReturnTareReasonVisibility();
                }
            }
            else
            {
                IsReturnTareReasonCategoryVisible = IsReturnTareReasonVisible = false;
                RemoveReturnTareReason();
            }
        }

        private void UpdateState()
        {
            bool canDisplayed;
            var canEdit = canDisplayed = Order.Status == OrderStatus.NewOrder;
            IsActionsForSaleVisible = IsDiscountVisible = canDisplayed;
            CanEditMovementItems = CanEditDepositItems = canEdit;
        }
    }
}
