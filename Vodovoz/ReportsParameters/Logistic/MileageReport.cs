using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using QS.Dialog.Gtk;
using QS.Dialog.GtkUI;
using QS.Report;
using QSReport;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Tdi;
using QS.ViewModels.Control.EEVM;
using QS.Widgets;
using Vodovoz.Domain.Employees;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.Filters.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ReportsParameters.Logistic
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MileageReport : SingleUoWWidgetBase, IParametersWidget
	{
		private readonly ILifetimeScope _scope;
		private readonly INavigationManager _navigationManager;
		private readonly ICarJournalFactory _carJournalFactory;
		private readonly ITdiTab _parrentDialog;
		private IEntityEntryViewModel _driverViewModel;
		
		public MileageReport(
			ILifetimeScope scope,
			INavigationManager navigationManager,
			ICarJournalFactory carJournalFactory,
			ITdiTab parrentDialog)
		{
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
			_carJournalFactory = carJournalFactory ?? throw new ArgumentNullException(nameof(carJournalFactory));
			_parrentDialog = parrentDialog ?? throw new ArgumentNullException(nameof(parrentDialog));

			Build();
			Configure();
		}

		private void Configure()
		{
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			ConfigureEntries();

			ycheckbutton1.Toggled += (sender, args) =>
			{
				entityviewmodelentryCar.Sensitive = !ycheckbutton1.Active;
				driverEntry.Sensitive = !ycheckbutton1.Active;
				entityviewmodelentryCar.Subject = null;
				_driverViewModel.Entity = null;
			};

			validatedentryDifference.ValidationMode = ValidationType.Numeric;
		}

		private void ConfigureEntries()
		{
			//TODO
			var builder = new LegacyEEVMBuilderFactory(_parrentDialog, UoW, _navigationManager, _scope);

			_driverViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel, EmployeeFilterViewModel>(
					x => x.Category = EmployeeCategory.driver,
					x => x.Status = EmployeeStatus.IsWorking)
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			driverEntry.ViewModel = _driverViewModel;
			//driverEntry.ViewModel.IsEditable = false;

			entityviewmodelentryCar.SetEntityAutocompleteSelectorFactory(_carJournalFactory.CreateCarAutocompleteSelectorFactory(_scope));
		}

		#region IParametersWidget implementation

		public event EventHandler<LoadReportEventArgs> LoadReport;

		public string Title {
			get {
				return "Отчет по километражу";
			}
		}

		private ReportInfo GetReportInfo()
		{
			var parameters = new Dictionary<string, object>
			{
				{ "start_date", dateperiodpicker.StartDateOrNull },
				{ "end_date", dateperiodpicker.EndDateOrNull },
				{ "our_cars_only", ycheckbutton1.Active },
				{ "any_status", checkAnyStatus.Active },
				{ "car_id", entityviewmodelentryCar.Subject.GetIdOrNull() ?? 0 },
				{ "employee_id", _driverViewModel.Entity.GetIdOrNull() ?? 0 }
			};

			int temp = 0;
			if (!String.IsNullOrEmpty(validatedentryDifference.Text) && validatedentryDifference.Text.All(char.IsDigit))
			{
				temp = int.Parse(validatedentryDifference.Text);
			}
			parameters.Add("difference_km", validatedentryDifference.Text);
			
			return new ReportInfo {
				Identifier = "Logistic.MileageReport",
				UseUserVariables = true,
				Parameters = parameters
			};
		}

		protected void OnButtonCreateReportClicked(object sender, EventArgs e)
		{
			OnUpdate(true);
		}

		void OnUpdate(bool hide = false)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
		}

		void CanRun()
		{
			buttonCreateReport.Sensitive =
				(dateperiodpicker.EndDateOrNull != null && dateperiodpicker.StartDateOrNull != null);
		}

		protected void OnDateperiodpickerPeriodChanged(object sender, EventArgs e)
		{
			CanRun();
		}

		#endregion
	}
}
