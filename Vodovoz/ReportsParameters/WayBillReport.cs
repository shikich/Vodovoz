using System;
using System.Collections.Generic;
using Autofac;
using QS.Report;
using QSReport;
using QS.Dialog.GtkUI;
using QS.DomainModel.Entity;
using QS.Project.Journal.EntitySelector;
using Vodovoz.Domain.Employees;
using Vodovoz.Filters.ViewModels;
using QS.Project.Services;
using Vodovoz.Domain.Logistic;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Tdi;
using QS.ViewModels.Control.EEVM;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Journals.Filters.Cars;
using Vodovoz.ViewModels.Journals.Filters.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ReportsParameters
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WayBillReport : SingleUoWWidgetBase, IParametersWidget
	{
		private readonly ILifetimeScope _scope;
		private readonly INavigationManager _navigationManager;
		private readonly ITdiTab _parrentDialog;
		private IEntityEntryViewModel _driverViewModel;
		
		public WayBillReport(
			ILifetimeScope scope,
			INavigationManager navigationManager,
			ITdiTab parrentDialog)
		{
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
			_parrentDialog = parrentDialog ?? throw new ArgumentNullException(nameof(parrentDialog));

			Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			Configure();
		}

		private void Configure()
		{
			datepicker.Date = DateTime.Today;
			timeHourEntry.Text = DateTime.Now.Hour.ToString("00.##");
			timeMinuteEntry.Text = DateTime.Now.Minute.ToString("00.##");

			ConfigureDriverEntry();

			entryCar.SetEntityAutocompleteSelectorFactory(new DefaultEntityAutocompleteSelectorFactory
				<Car, CarJournalViewModel, CarJournalFilterViewModel>(ServicesConfig.CommonServices));
		}

		private void ConfigureDriverEntry()
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
		}

		#region IParametersWidget implementation

		public event EventHandler<LoadReportEventArgs> LoadReport;

		public string Title {
			get {
				return "Путевой лист";
			}
		}

		#endregion

		private ReportInfo GetReportInfo()
		{
			return new ReportInfo {
				Identifier = "Logistic.WayBillReport",
				Parameters = new Dictionary<string, object>
				{
					{ "date", datepicker.Date },
					{ "driver_id", _driverViewModel.Entity.GetIdOrNull() ?? -1 },
					{ "car_id", entryCar.Subject.GetIdOrNull() ?? -1 },
					{ "time", timeHourEntry.Text + ":" + timeMinuteEntry.Text },
					{ "need_date", !datepicker.IsEmpty }
				}
			};
		}

		void OnUpdate(bool hide = false)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
		}

		protected void OnButtonCreateRepotClicked(object sender, EventArgs e)
		{
			OnUpdate(true);
		}
	}
}
