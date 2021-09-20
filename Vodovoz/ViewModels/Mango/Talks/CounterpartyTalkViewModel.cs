using System;
using System.Collections.Generic;
using System.Linq;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QSReport;
using Vodovoz.Dialogs.Sale;
using Vodovoz.Domain.Client;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.Infrastructure.Mango;
using Vodovoz.JournalNodes;
using Vodovoz.JournalViewModels;
using Vodovoz.Parameters;
using Vodovoz.Services;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Complaints;
using Vodovoz.Views.Mango;

namespace Vodovoz.ViewModels.Mango.Talks
{
	public class CounterpartyTalkViewModel : TalkViewModelBase, IDisposable
	{
		private readonly ITdiCompatibilityNavigation _tdiNavigation;
		private readonly IRouteListRepository _routedListRepository;
		private readonly IInteractiveService _interactiveService;
		private readonly IOrderParametersProvider _orderParametersProvider;
		private readonly IEmployeeJournalFactory _employeeJournalFactory;
		private readonly ICounterpartyJournalFactory _counterpartyJournalFactory;
		private readonly INomenclatureRepository _nomenclatureRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IParametersProvider _parametersProvider;
		private readonly IUnitOfWork _uow;
		private bool _isDisposed;
		private IPage _newOrderPage;
		private IPage _newCounterpartyPage;
		private CounterpartyJournalViewModel _counterpartyJournal;

		public List<CounterpartyOrderViewModel> CounterpartyOrdersViewModels { get; private set; } = new List<CounterpartyOrderViewModel>();

		public Counterparty currentCounterparty { get;private set; }
		public event Action CounterpartyOrdersModelsUpdateEvent = () => { };

		public CounterpartyTalkViewModel(
			ITdiCompatibilityNavigation tdinavigation,
			IUnitOfWorkFactory unitOfWorkFactory,
			IRouteListRepository routedListRepository,
			IInteractiveService interactiveService,
			IOrderParametersProvider orderParametersProvider, 
			MangoManager manager,
			IEmployeeJournalFactory employeeJournalFactory,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			INomenclatureRepository nomenclatureRepository,
			IOrderRepository orderRepository,
			IParametersProvider parametersProvider) : base(tdinavigation, manager)
		{
			_tdiNavigation = tdinavigation ?? throw new ArgumentNullException(nameof(tdinavigation));
			_routedListRepository = routedListRepository ?? throw new ArgumentNullException(nameof(routedListRepository));
			_interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
			_orderParametersProvider = orderParametersProvider ?? throw new ArgumentNullException(nameof(orderParametersProvider));
			_employeeJournalFactory = employeeJournalFactory ?? throw new ArgumentNullException(nameof(employeeJournalFactory));
			_counterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));
			_nomenclatureRepository = nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
			_orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_parametersProvider = parametersProvider ?? throw new ArgumentNullException(nameof(parametersProvider));
			_uow = unitOfWorkFactory.CreateWithoutRoot();

			/*if(ActiveCall.CounterpartyIds.Any())
			{*/
				var clients = _uow.GetById<Counterparty>(new []{118414}/*ActiveCall.CounterpartyIds*/);
				
				foreach(Counterparty client in clients)
				{
					CounterpartyOrderViewModel model = new CounterpartyOrderViewModel(
						client, unitOfWorkFactory, tdinavigation, routedListRepository, MangoManager, _orderParametersProvider,
						_employeeJournalFactory, _counterpartyJournalFactory, _nomenclatureRepository, _parametersProvider);
					CounterpartyOrdersViewModels.Add(model);
				}
				
				currentCounterparty = CounterpartyOrdersViewModels.FirstOrDefault().Client;
			/*} else
				throw new InvalidProgramException("Открыт диалог разговора с имеющимся контрагентом, но ни одного id контрагента не найдено.");
		*/}

		public IDictionary<string, CounterpartyOrderView> GetCounterpartyViewModels()
		{
			return null;
		}
		#region Взаимодействие с Mangos

		#endregion

		#region Действия View

		public void UpadateCurrentCounterparty(Counterparty counterparty)
		{
			currentCounterparty = counterparty;

		}
		public void NewClientCommand()
		{
			_newCounterpartyPage = _tdiNavigation.OpenTdiTab<CounterpartyDlg>(this);
			_newCounterpartyPage.PageClosed += NewCounerpatryPageClosed;
		}

		public void ExistingClientCommand()
		{
			_counterpartyJournal = NavigationManager.OpenViewModel<CounterpartyJournalViewModel>(null).ViewModel;
			_counterpartyJournal.SelectionMode = QS.Project.Journal.JournalSelectionMode.Single;
			_counterpartyJournal.OnEntitySelectedResult += ExistingCounterpartyPageClosed;
		}

		void NewCounerpatryPageClosed(object sender, PageClosedEventArgs e)
		{
			if(e.CloseSource == CloseSource.Save) {
				List<Counterparty> clients = new List<Counterparty>();
				Counterparty client = ((sender as TdiTabPage).TdiTab as CounterpartyDlg).Counterparty;
				client.Phones.Add(ActiveCall.Phone);
				clients.Add(client);
				_uow.Save<Counterparty>(client);
				
				CounterpartyOrderViewModel model = 
					new CounterpartyOrderViewModel(
						client,
						UnitOfWorkFactory.GetDefaultFactory,
						_tdiNavigation,
						_routedListRepository,
						MangoManager,
						_orderParametersProvider,
						_employeeJournalFactory,
						_counterpartyJournalFactory,
						_nomenclatureRepository,
						_parametersProvider);
				
				CounterpartyOrdersViewModels.Add(model);
				currentCounterparty = client;
				MangoManager.AddCounterpartyToCall(client.Id);
				CounterpartyOrdersModelsUpdateEvent();
			}
			(sender as IPage).PageClosed -= NewCounerpatryPageClosed;
		}

		void ExistingCounterpartyPageClosed(object sender, QS.Project.Journal.JournalSelectedNodesEventArgs e)
		{
			var counterpartyNode = e.SelectedNodes.First() as CounterpartyJournalNode;
			Counterparty client = _uow.GetById<Counterparty>(counterpartyNode.Id);
			if(!CounterpartyOrdersViewModels.Any(c => c.Client.Id == client.Id)) {
				if(_interactiveService.Question($"Добавить телефон к контрагенту {client.Name} ?", "Телефон контрагента")) {
					client.Phones.Add(ActiveCall.Phone);
					_uow.Save<Counterparty>(client);
					_uow.Commit();
				}
				
				CounterpartyOrderViewModel model =
					new CounterpartyOrderViewModel(
						client, UnitOfWorkFactory.GetDefaultFactory, _tdiNavigation, _routedListRepository, MangoManager,
						_orderParametersProvider, _employeeJournalFactory, _counterpartyJournalFactory, _nomenclatureRepository,
						_parametersProvider);
				
				CounterpartyOrdersViewModels.Add(model);
				currentCounterparty = client;
				MangoManager.AddCounterpartyToCall(client.Id);
				CounterpartyOrdersModelsUpdateEvent();
			}
		}

		public void NewOrderCommand()
		{
			if (currentCounterparty.IsForRetail)
			{
				_interactiveService.ShowMessage(ImportanceLevel.Warning, "Заказ поступает от контрагента дистрибуции");
			}
			
			_newOrderPage = _tdiNavigation.OpenTdiTab<OrderDlg, Counterparty>(null, currentCounterparty);
			_newOrderPage.PageClosed += OnNewOrderPageClosed;
		}

		private void OnNewOrderPageClosed(object sender, PageClosedEventArgs e)
		{
			if(_isDisposed)
			{
				return;
			}
			
			var model = CounterpartyOrdersViewModels.Find(m => m.Client.Id == currentCounterparty.Id);
			model.RefreshOrders();
		}

		public void AddComplainCommand()
		{
			var employeeSelectorFactory = _employeeJournalFactory.CreateEmployeeAutocompleteSelectorFactory();

			var counterpartySelectorFactory = _counterpartyJournalFactory.CreateCounterpartyAutocompleteSelectorFactory();

			var parameters = new Dictionary<string, object> {
				{"client", currentCounterparty},
				{"uowBuilder", EntityUoWBuilder.ForCreate()},
				{ "unitOfWorkFactory", UnitOfWorkFactory.GetDefaultFactory },
				//Autofac: IEmployeeService 
				{"employeeSelectorFactory", employeeSelectorFactory},
				{"counterpartySelectorFactory", counterpartySelectorFactory},
				//Autofac: ICommonServices
				//Autofac: IUserRepository
				{"phone", "+7" + ActiveCall.Phone.Number }
			};
			
			_tdiNavigation.OpenTdiTabOnTdiNamedArgs<CreateComplaintViewModel>(null,parameters);
		}

		public void BottleActCommand()
		{
			var parameters = new Vodovoz.Reports.RevisionBottlesAndDeposits(_orderRepository);
			parameters.SetCounterparty(currentCounterparty);
			ReportViewDlg dialog = _tdiNavigation.OpenTdiTab<ReportViewDlg, IParametersWidget>(null, parameters) as ReportViewDlg;
			parameters.OnUpdate(true);
			
		}

		public void StockBalanceCommand()
		{
			NomenclatureStockFilterViewModel filter = new NomenclatureStockFilterViewModel(new WarehouseSelectorFactory());
			NavigationManager.OpenViewModel<NomenclatureStockBalanceJournalViewModel, NomenclatureStockFilterViewModel>(null, filter);
		}

		public void CostAndDeliveryIntervalCommand(DeliveryPoint point)
		{
			_tdiNavigation.OpenTdiTab<DeliveryPriceDlg, DeliveryPoint>(null, point);
		}

		#endregion

		public void Dispose()
		{
			if(_isDisposed)
			{
				return;
			}
			if(_newOrderPage != null)
			{
				_newOrderPage.PageClosed -= OnNewOrderPageClosed;
			}
			if(_newCounterpartyPage != null)
			{
				_newCounterpartyPage.PageClosed -= NewCounerpatryPageClosed;
			}
			if(_counterpartyJournal != null)
			{
				_counterpartyJournal.OnEntitySelectedResult -= ExistingCounterpartyPageClosed;
			}
			foreach(var viewModel in CounterpartyOrdersViewModels)
			{
				viewModel.Dispose();
			}
			
			_uow?.Dispose();
			_isDisposed = true;
		}
	}
}
