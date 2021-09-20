using System;
using System.Collections.Generic;
using System.Linq;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using Vodovoz.Dialogs.Sale;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.Infrastructure.Mango;
using Vodovoz.JournalNodes;
using Vodovoz.JournalViewModels;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Complaints;

namespace Vodovoz.ViewModels.Mango.Talks
{
	public class UnknowTalkViewModel : TalkViewModelBase, IDisposable
	{
		private readonly ITdiCompatibilityNavigation _tdiNavigation;
		private readonly IInteractiveQuestion _interactive;
		private readonly IEmployeeJournalFactory _employeeJournalFactory;
		private readonly ICounterpartyJournalFactory _counterpartyJournalFactory;
		private readonly IUnitOfWork _uow;

		private bool _isDisposed;
		private IPage _newCounterpartyPage;
		private CounterpartyJournalViewModel _counterpartyJournal;
		
		public UnknowTalkViewModel(IUnitOfWorkFactory unitOfWorkFactory, 
			ITdiCompatibilityNavigation navigation, 
			IInteractiveQuestion interactive,
			MangoManager manager,
			IEmployeeJournalFactory employeeJournalFactory,
			ICounterpartyJournalFactory counterpartyJournalFactory) : base(navigation, manager)
		{
			_tdiNavigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
			_interactive = interactive ?? throw new ArgumentNullException(nameof(interactive));
			_employeeJournalFactory = employeeJournalFactory ?? throw new ArgumentNullException(nameof(employeeJournalFactory));
			_counterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));
			_uow = unitOfWorkFactory.CreateWithoutRoot();
		}

		#region Действия View

		public void SelectNewConterparty()
		{
			_newCounterpartyPage = _tdiNavigation.OpenTdiTab<CounterpartyDlg,Phone>(this, ActiveCall.Phone);
			_newCounterpartyPage.PageClosed += NewCounerpatryPageClosed;
		}

		public void SelectExistConterparty()
		{
			_counterpartyJournal = NavigationManager.OpenViewModel<CounterpartyJournalViewModel>(null).ViewModel;
			_counterpartyJournal.SelectionMode = QS.Project.Journal.JournalSelectionMode.Single;
			_counterpartyJournal.OnEntitySelectedResult += ExistingCounterpartyPageClosed;
		}

		void NewCounerpatryPageClosed(object sender, PageClosedEventArgs e)
		{ 
			if(e.CloseSource == CloseSource.Save) {
				Counterparty client = ((sender as TdiTabPage).TdiTab as CounterpartyDlg).Counterparty;
				if(client != null) {
					this.Close(true, CloseSource.External);
					MangoManager.AddCounterpartyToCall(client.Id);
				} else
					throw new Exception("При сохранении контрагента произошла ошибка, попробуйте снова." + "\n Сообщение для поддержки : UnknowTalkViewModel.NewCounterparty_PageClose()");
			}
		}

		void ExistingCounterpartyPageClosed(object sender, QS.Project.Journal.JournalSelectedNodesEventArgs e)
		{
			var counterpartyNode = e.SelectedNodes.First() as CounterpartyJournalNode;
			Counterparty client = _uow.GetById<Counterparty>(counterpartyNode.Id);
			if(_interactive.Question($"Добавить телефон к контрагенту {client.Name} ?", "Телефон контрагента")) {
				if(!client.Phones.Any(phone => phone.DigitsNumber == ActiveCall.Phone.DigitsNumber)) {
					client.Phones.Add(ActiveCall.Phone);
					_uow.Save<Counterparty>(client);
					_uow.Commit();
				}
			}
			this.Close(true, CloseSource.External);
			MangoManager.AddCounterpartyToCall(client.Id);
		}

		public void CreateComplaintCommand()
		{
			var employeeSelectorFactory = _employeeJournalFactory.CreateEmployeeAutocompleteSelectorFactory();

			var counterpartySelectorFactory = _counterpartyJournalFactory.CreateCounterpartyAutocompleteSelectorFactory();

			var parameters = new Dictionary<string, object> {
				{"uowBuilder", EntityUoWBuilder.ForCreate()},
				{ "unitOfWorkFactory", UnitOfWorkFactory.GetDefaultFactory },
				//Autofac: IEmployeeService 
				{"employeeSelectorFactory", employeeSelectorFactory},
				{"counterpartySelectorFactory", counterpartySelectorFactory},
				//Autofac: ICommonServices
				//Autofac: IUserRepository
				{"phone", "+7" + ActiveCall.Phone.Number }
			};
			
			_tdiNavigation.OpenTdiTabOnTdiNamedArgs<CreateComplaintViewModel>(null, parameters);
		}

		public void StockBalanceCommand()
		{
			NomenclatureStockFilterViewModel filter = new NomenclatureStockFilterViewModel(new WarehouseSelectorFactory());
			NavigationManager.OpenViewModel<NomenclatureStockBalanceJournalViewModel, NomenclatureStockFilterViewModel>(null, filter);
		}

		public void CostAndDeliveryIntervalCommand()
		{
			_tdiNavigation.OpenTdiTab<DeliveryPriceDlg>(null);
		}

		#endregion

		public void Dispose()
		{
			if(_isDisposed)
			{
				return;
			}
			if(_newCounterpartyPage != null)
			{
				_newCounterpartyPage.PageClosed -= NewCounerpatryPageClosed;
			}
			if(_counterpartyJournal != null)
			{
				_counterpartyJournal.OnEntitySelectedResult -= ExistingCounterpartyPageClosed;
			}
			
			_uow?.Dispose();
			_isDisposed = true;
		}
	}
}
