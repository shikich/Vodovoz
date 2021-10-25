using System;
using System.Collections.Generic;
using Autofac;
using QS.Dialog.GtkUI;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using QS.Report;
using QS.Tdi;
using QS.ViewModels.Control.EEVM;
using QSReport;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.JournalViewModels;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.Filters.Counterparties;
using Vodovoz.ViewModels.Journals.Filters.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ReportsParameters
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ChainStoreDelayReport : SingleUoWWidgetBase, IParametersWidget
	{
		private readonly ILifetimeScope _scope;
		private readonly INavigationManager _navigationManager;
		private readonly ICounterpartyJournalFactory _counterpartyJournalFactory;
		private readonly ITdiTab _parrentDialog;
		private IEntityEntryViewModel _salesManegerViewModel;
		private IEntityEntryViewModel _orderAuthorViewModel;
		
		public ChainStoreDelayReport(
			ILifetimeScope scope,
			INavigationManager navigationManager,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			ITdiTab parrentDialog)
		{
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
			_counterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));
			_parrentDialog = parrentDialog ?? throw new ArgumentNullException(nameof(parrentDialog));
			Build();
			Configure();
		}

		private void Configure()
		{
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			ydatepicker.Date = DateTime.Now.Date;
			ConfigureEntries();
			ydatepicker.Date = DateTime.Now;
			buttonRun.Sensitive = true;
			buttonRun.Clicked += OnButtonCreateReportClicked;
		}

		private void ConfigureEntries()
		{
			entityviewmodelentryCounterparty.SetEntityAutocompleteSelectorFactory(
				_counterpartyJournalFactory.CreateCounterpartyAutocompleteSelectorFactory(_scope));

			var builder = new LegacyEEVMBuilderFactory(_parrentDialog, UoW, _navigationManager, _scope);
			var filterParams = new Action<EmployeeFilterViewModel>[]
			{
				x => x.Category = EmployeeCategory.office,
				x => x.Status = EmployeeStatus.IsWorking
			};

			//TODO проверить работоспособность
			_salesManegerViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel, EmployeeFilterViewModel>(filterParams)
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			salesManagerEntry.ViewModel = _salesManegerViewModel;
			//salesManagerEntry.ViewModel.IsEditable = false;
			
			_orderAuthorViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel, EmployeeFilterViewModel>(filterParams)
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			orderAuthorEntry.ViewModel = _orderAuthorViewModel;
			//orderAuthorEntry.ViewModel.IsEditable = false;
		}

		void OnButtonCreateReportClicked (object sender, EventArgs e)
		{
			OnUpdate (true);
		}

		#region IParametersWidget implementation

		public string Title => "Отсрочка сети";

		public event EventHandler<LoadReportEventArgs> LoadReport;

		#endregion

		private ReportInfo GetReportInfo()
		{
			return new ReportInfo {
				Identifier = "Payments.PaymentsDelayNetwork",
				Parameters = new Dictionary<string, object>
				{
					{ "date", ydatepicker.Date },
					{ "counterparty_id", entityviewmodelentryCounterparty.Subject.GetIdOrNull() ?? -1 },
					{ "sell_manager_id", _salesManegerViewModel.Entity.GetIdOrNull() ?? -1 },
					{ "order_author_id", _orderAuthorViewModel.Entity.GetIdOrNull() ?? -1 }
				}
			};
		}

		void OnUpdate(bool hide = false) => LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
	}
}
