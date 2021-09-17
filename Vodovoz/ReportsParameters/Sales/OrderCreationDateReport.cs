﻿using System;
using System.Collections.Generic;
using QS.Report;
using QSReport;
using Vodovoz.Domain.Employees;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.FilterViewModels.Employees;

namespace Vodovoz.ReportsParameters.Sales
{
	public partial class OrderCreationDateReport : Gtk.Bin, IParametersWidget
	{
		public OrderCreationDateReport()
		{
			this.Build();
			var driverFilter = new EmployeeFilterViewModel
			{
				RestrictCategory = EmployeeCategory.office,
				Status = EmployeeStatus.IsWorking
			};
			var employeeFactory = new EmployeeJournalFactory(driverFilter);
			evmeEmployee.SetEntityAutocompleteSelectorFactory(employeeFactory.CreateEmployeeAutocompleteSelectorFactory());
			datePeriodPicker.PeriodChanged += (sender, e) => CanRun();
			buttonCreateReport.Clicked += (sender, e) => OnUpdate(true);
		}

		#region IParametersWidget implementation

		public event EventHandler<LoadReportEventArgs> LoadReport;

		public string Title => "Отчет по дате создания заказа";

		#endregion

		private ReportInfo GetReportInfo()
		{
			var parameters = new Dictionary<string, object> {
				{ "start_date", datePeriodPicker.StartDateOrNull },
				{ "end_date", datePeriodPicker.EndDateOrNull },
				{ "employee_id", (evmeEmployee.Subject as Employee)?.Id ?? 0 }
			};

			return new ReportInfo {
				Identifier = "Sales.OrderCreationDateReport",
				Parameters = parameters
			};
		}

		void OnUpdate(bool hide = false)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
		}

		void CanRun()
		{
			buttonCreateReport.Sensitive = datePeriodPicker.EndDateOrNull.HasValue && datePeriodPicker.StartDateOrNull.HasValue;
		}
	}
}
