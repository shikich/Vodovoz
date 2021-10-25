using System;
using System.Collections.Generic;
using Autofac;
using QS.Project.Filter;
using QS.Project.Journal.EntitySelector;
using QS.ViewModels.Control.EEVM;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.FilterViewModels.Enums;
using Vodovoz.ViewModels.Journals.JournalFactories;
using Vodovoz.ViewModels.Journals.JournalViewModels.Orders;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.ViewModels.Journals.Filters.Orders
{
	public class UndeliveredOrdersFilterViewModel : FilterViewModelBase<UndeliveredOrdersFilterViewModel>
	{
		private Order _restrictOldOrder;
		private Employee _restrictDriver;
		private Subdivision _restrictAuthorSubdivision;
		private Counterparty _restrictClient;
		private DeliveryPoint _restrictAddress;
		private Employee _restrictOldOrderAuthor;
		private DateTime? _restrictOldOrderStartDate;
		private DateTime? _restrictOldOrderEndDate;
		private DateTime? _restrictNewOrderStartDate;
		private DateTime? _restrictNewOrderEndDate;
		private GuiltyTypes? _restrictGuiltySide;
		private Subdivision _restrictGuiltyDepartment;
		private Subdivision _restrictInProcessAtDepartment;
		private UndeliveryStatus? _restrictUndeliveryStatus;
		private Employee _restrictUndeliveryAuthor;
		private bool _restrictIsProblematicCases;
		private bool _restrictNotIsProblematicCases;
		private ActionsWithInvoice? _restrictActionsWithInvoice;
		private bool _restrictGuiltyDepartmentVisible;

		public UndeliveredOrdersFilterViewModel(
			IOrderSelectorFactory orderSelectorFactory,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			IDeliveryPointJournalFactory deliveryPointJournalFactory,
			ISubdivisionJournalFactory subdivisionJournalFactory,
			UndeliveredOrdersFilterViewModelParameters filterParams,
			ILifetimeScope scope)
		{
			OrderSelectorFactory = (orderSelectorFactory ?? throw new ArgumentNullException(nameof(orderSelectorFactory)))
				.CreateOrderAutocompleteSelectorFactory(scope);

			CounterpartySelectorFactory = (counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory)))
				.CreateCounterpartyAutocompleteSelectorFactory(scope);

			DeliveryPointSelectorFactory = (deliveryPointJournalFactory ?? throw new ArgumentNullException(nameof(deliveryPointJournalFactory)))
				.CreateDeliveryPointAutocompleteSelectorFactory(scope);

			AuthorSubdivisionSelectorFactory = (subdivisionJournalFactory ?? throw new ArgumentNullException(nameof(subdivisionJournalFactory)))
				.CreateDefaultSubdivisionAutocompleteSelectorFactory(scope);

			Subdivisions = UoW.GetAll<Subdivision>();
			SetAndRefilterAtOnce(filterParams.FilterParamsForUndeliveries.FilterParams);
		}

		public IEntityAutocompleteSelectorFactory OrderSelectorFactory { get; }
		public IEntityEntryViewModel DriverEmployeeViewModel { get; set; }
		public IEntityEntryViewModel UndeliveryAuthorViewModel { get; set; }
		public IEntityEntryViewModel OldOrderAuthorViewModel { get; set; }
		public IEntityAutocompleteSelectorFactory CounterpartySelectorFactory { get; }
		public IEntityAutocompleteSelectorFactory DeliveryPointSelectorFactory { get; }
		public IEntityAutocompleteSelectorFactory AuthorSubdivisionSelectorFactory { get; }

		public Order RestrictOldOrder
		{
			get => _restrictOldOrder;
			set => UpdateFilterField(ref _restrictOldOrder, value);
		}

		public Employee RestrictDriver
		{
			get => _restrictDriver;
			set => UpdateFilterField(ref _restrictDriver, value);
		}

		public Subdivision RestrictAuthorSubdivision
		{
			get => _restrictAuthorSubdivision;
			set => UpdateFilterField(ref _restrictAuthorSubdivision, value);
		}

		public Counterparty RestrictClient
		{
			get => _restrictClient;
			set => UpdateFilterField(ref _restrictClient, value);
		}

		public DeliveryPoint RestrictAddress
		{
			get => _restrictAddress;
			set => UpdateFilterField(ref _restrictAddress, value);
		}

		public Employee RestrictOldOrderAuthor
		{
			get => _restrictOldOrderAuthor;
			set => UpdateFilterField(ref _restrictOldOrderAuthor, value);
		}

		public DateTime? RestrictOldOrderStartDate
		{
			get => _restrictOldOrderStartDate;
			set => UpdateFilterField(ref _restrictOldOrderStartDate, value);
		}

		public DateTime? RestrictOldOrderEndDate
		{
			get => _restrictOldOrderEndDate;
			set => UpdateFilterField(ref _restrictOldOrderEndDate, value);
		}

		public DateTime? RestrictNewOrderStartDate
		{
			get => _restrictNewOrderStartDate;
			set => UpdateFilterField(ref _restrictNewOrderStartDate, value);
		}

		public DateTime? RestrictNewOrderEndDate
		{
			get => _restrictNewOrderEndDate;
			set => UpdateFilterField(ref _restrictNewOrderEndDate, value);
		}

		public GuiltyTypes? RestrictGuiltySide
		{
			get => _restrictGuiltySide;
			set
			{
				if(value == GuiltyTypes.Department)
				{
					RestrictGuiltyDepartmentVisible = true;
				}
				else
				{
					RestrictGuiltyDepartmentVisible = false;
					RestrictGuiltyDepartment = null;
				}
				UpdateFilterField(ref _restrictGuiltySide, value);
			}
		}

		public bool RestrictGuiltyDepartmentVisible
		{
			get => _restrictGuiltyDepartmentVisible;
			set => UpdateFilterField(ref _restrictGuiltyDepartmentVisible, value);
		}

		public Subdivision RestrictGuiltyDepartment
		{
			get => _restrictGuiltyDepartment;
			set => UpdateFilterField(ref _restrictGuiltyDepartment, value);
		}

		public Subdivision RestrictInProcessAtDepartment
		{
			get => _restrictInProcessAtDepartment;
			set => UpdateFilterField(ref _restrictInProcessAtDepartment, value);
		}

		public ActionsWithInvoice? RestrictActionsWithInvoice
		{
			get => _restrictActionsWithInvoice;
			set
			{
				switch(value)
				{
					case ActionsWithInvoice.createdNew:
						NewInvoiceCreated = true;
						break;
					case ActionsWithInvoice.notCreated:
						NewInvoiceCreated = false;
						break;
					default:
						NewInvoiceCreated = null;
						break;
				}
				UpdateFilterField(ref _restrictActionsWithInvoice, value);
			}
		}

		public bool? NewInvoiceCreated { get; set; }

		public UndeliveryStatus? RestrictUndeliveryStatus
		{
			get => _restrictUndeliveryStatus;
			set => UpdateFilterField(ref _restrictUndeliveryStatus, value);
		}

		public Employee RestrictUndeliveryAuthor
		{
			get => _restrictUndeliveryAuthor;
			set => UpdateFilterField(ref _restrictUndeliveryAuthor, value);
		}

		public bool RestrictIsProblematicCases
		{
			get => _restrictIsProblematicCases;
			set
			{
				if(value)
				{
					RestrictGuiltySide = null;
				}

				RestrictNotIsProblematicCases = !value;

				UpdateFilterField(ref _restrictIsProblematicCases, value);
			}
		}

		public bool RestrictNotIsProblematicCases
		{
			get => _restrictNotIsProblematicCases;
			set => UpdateFilterField(ref _restrictNotIsProblematicCases, value);
		}

		public IEnumerable<Subdivision> Subdivisions { get; private set; }

		public GuiltyTypes[] ExcludingGuiltiesForProblematicCases => new GuiltyTypes[] { GuiltyTypes.Client, GuiltyTypes.None };
	}
}
