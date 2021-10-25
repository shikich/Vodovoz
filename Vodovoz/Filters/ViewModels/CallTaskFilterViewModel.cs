using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Autofac;
using QS.Commands;
using QS.Navigation;
using QS.Project.Filter;
using QS.Tdi;
using QS.Utilities;
using QS.ViewModels.Control.EEVM;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Sale;
using Vodovoz.EntityRepositories.Counterparties;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.Filters.ViewModels
{
	public class CallTaskFilterViewModel : FilterViewModelBase<CallTaskFilterViewModel>
	{
		private TaskFilterDateType _dateType = TaskFilterDateType.DeadlinePeriod;
		private DeliveryPointCategory _deliveryPointCategory;
		private Employee _employee;
		private DateTime _endDate;
		private bool _hideCompleted = true;
		private bool _showOnlyWithoutEmployee = true;
		private SortingDirectionType _sortingDirection;
		private SortingParamType _sortingParam = SortingParamType.Id;
		private DateTime _startDate;
		private GeographicGroup _geographicGroup;

		public CallTaskFilterViewModel(
			IDeliveryPointRepository deliveryPointRepository,
			ILifetimeScope scope,
			ITdiTab tdiTab,
			INavigationManager navigationManager)
		{
			if(navigationManager == null)
			{
				throw new ArgumentNullException(nameof(navigationManager));
			}

			ActiveDeliveryPointCategories =
				deliveryPointRepository?.GetActiveDeliveryPointCategories(UoW)
				?? throw new ArgumentNullException(nameof(deliveryPointRepository));
			
			StartDate = DateTime.Today.AddDays(-14);
			EndDate = DateTime.Today.AddDays(14);
			GeographicGroups = UoW.Session.QueryOver<GeographicGroup>().List();

			var builder =
				new LegacyEEVMBuilderFactory<CallTaskFilterViewModel>(tdiTab, this, UoW, navigationManager, scope);

			EmployeeEntryViewModel = builder.ForProperty(x => x.Employee)
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel>()
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			CreateCommands();
		}

		public IEntityEntryViewModel EmployeeEntryViewModel { get; }
		public IOrderedEnumerable<DeliveryPointCategory> ActiveDeliveryPointCategories { get; }

		public DateTime StartDate
		{
			get => _startDate;
			set => UpdateFilterField(ref _startDate, value);
		}

		public DateTime EndDate
		{
			get => _endDate;
			set => UpdateFilterField(ref _endDate, value);
		}

		public TaskFilterDateType DateType
		{
			get => _dateType;
			set => UpdateFilterField(ref _dateType, value);
		}

		public Employee Employee
		{
			get => _employee;
			set => UpdateFilterField(ref _employee, value);
		}

		public DeliveryPointCategory DeliveryPointCategory
		{
			get => _deliveryPointCategory;
			set => UpdateFilterField(ref _deliveryPointCategory, value);
		}

		public bool HideCompleted
		{
			get => _hideCompleted;
			set => UpdateFilterField(ref _hideCompleted, value);
		}

		public bool ShowOnlyWithoutEmployee
		{
			get => _showOnlyWithoutEmployee;
			set => UpdateFilterField(ref _showOnlyWithoutEmployee, value);
		}

		public SortingParamType SortingParam
		{
			get => _sortingParam;
			set => UpdateFilterField(ref _sortingParam, value);
		}

		public SortingDirectionType SortingDirection
		{
			get => _sortingDirection;
			set => UpdateFilterField(ref _sortingDirection, value);
		}

		public GeographicGroup GeographicGroup
		{
			get => _geographicGroup;
			set => UpdateFilterField(ref _geographicGroup, value);
		}

		public IList<GeographicGroup> GeographicGroups { get; }

		private void SetDatePeriod(DateTime startPeriod, DateTime endPeriod)
		{
			StartDate = startPeriod;
			EndDate = endPeriod;
		}

		public void SetDatePeriod(int weekIndex)
		{
			DateHelper.GetWeekPeriod(out DateTime start_date, out DateTime end_date, weekIndex);
			SetDatePeriod(start_date, end_date);
		}

		private void CreateCommands()
		{
			CreateChangeDateOnExpiredCommand();
			CreateChangeDateOnTodayCommand();
			CreateChangeDateOnTomorrowCommand();
			CreateChangeDateOnThisWeekCommand();
			CreateChangeDateOnNextWeekCommand();
		}

		#region Commands

		public DelegateCommand ChangeDateOnExpiredCommand { get; private set; }

		private void CreateChangeDateOnExpiredCommand()
		{
			ChangeDateOnExpiredCommand = new DelegateCommand(
				() =>
				{
					StartDate = DateTime.Now.AddDays(-15);
					EndDate = DateTime.Now.AddDays(-1);
				}, () => true
			);
		}

		public DelegateCommand ChangeDateOnTodayCommand { get; private set; }

		private void CreateChangeDateOnTodayCommand()
		{
			ChangeDateOnTodayCommand = new DelegateCommand(
				() =>
				{
					StartDate = DateTime.Now;
					EndDate = DateTime.Now;
				}, () => true
			);
		}

		public DelegateCommand ChangeDateOnTomorrowCommand { get; private set; }

		private void CreateChangeDateOnTomorrowCommand()
		{
			ChangeDateOnTomorrowCommand = new DelegateCommand(
				() =>
				{
					StartDate = DateTime.Now.AddDays(1);
					EndDate = DateTime.Now.AddDays(1);
				}, () => true
			);
		}

		public DelegateCommand ChangeDateOnThisWeekCommand { get; private set; }

		private void CreateChangeDateOnThisWeekCommand()
		{
			ChangeDateOnThisWeekCommand = new DelegateCommand(
				() =>
				{
					DateHelper.GetWeekPeriod(out DateTime start_date, out DateTime end_date, 0);
					StartDate = start_date;
					EndDate = end_date;
				}, () => true
			);
		}

		public DelegateCommand ChangeDateOnNextWeekCommand { get; private set; }

		private void CreateChangeDateOnNextWeekCommand()
		{
			ChangeDateOnNextWeekCommand = new DelegateCommand(
				() =>
				{
					DateHelper.GetWeekPeriod(out DateTime start_date, out DateTime end_date, 1);
					StartDate = start_date;
					EndDate = end_date;
				}, () => true
			);
		}

		#endregion
	}

	public enum TaskFilterDateType
	{
		[Display(Name = "Дата создания задачи")]
		CreationTime,
		[Display(Name = "Дата выполнения задачи")]
		CompleteTaskDate,
		[Display(Name = "Период выполнения задачи")]
		DeadlinePeriod
	}

	public enum SortingParamType
	{
		[Display(Name = "Клиент")] Client,
		[Display(Name = "Адрес")] DeliveryPoint,
		[Display(Name = "№")] Id,
		[Display(Name = "Долг по адресу")] DebtByAddress,
		[Display(Name = "Долг по клиенту")] DebtByClient,
		[Display(Name = "Создатель задачи")] Deadline,
		[Display(Name = "Ответственный")] AssignedEmployee,
		[Display(Name = "Статус")] Status,
		[Display(Name = "Срочность")] ImportanceDegree
	}

	public enum SortingDirectionType
	{
		[Display(Name = "От меньшего к большему")]
		FromSmallerToBigger,
		[Display(Name = "От большего к меньшему")]
		FromBiggerToSmaller
	}
}
