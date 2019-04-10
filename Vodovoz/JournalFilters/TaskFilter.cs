﻿using System;
using QS.Utilities;
using Vodovoz.Domain.Employees;
using Vodovoz.ViewModel;

namespace Vodovoz.JournalFilters
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class TaskFilter : Gtk.Bin
	{
		public event Action FilterChanged;

		public TaskFilter()
		{
			this.Build();
			FilterChanged += () => {
				if(StartActivePerionDate != null) dateperiodpickerFilter.StartDate = StartActivePerionDate.Value;
				if(StartActivePerionDate != null) dateperiodpickerFilter.EndDate = EndActivePeriodDate.Value;
			};
			EmployeesVM employeeVM = new EmployeesVM();
			employeeVM.Filter.RestrictCategory = EmployeeCategory.office;
			entryreferencevmEmployee.RepresentationModel = employeeVM;
		}

		public bool HideCompleted { get { return checkbuttonHideCompleted.Active; } }

		public Employee Employee { get { return entryreferencevmEmployee.Subject as Employee; } }

		public DateTime? StartActivePerionDate;

		public DateTime? EndActivePeriodDate;

		public DateTime? StartTaskCreateDate;

		public DateTime? EndTaskCreateDate;

		protected void OnButtonExpiredClicked(object sender, EventArgs e)
		{
			StartActivePerionDate = DateTime.Now.AddDays(-15);
			EndActivePeriodDate = DateTime.Now;
			FilterChanged?.Invoke();
		}

		protected void OnButtonTodayClicked(object sender, EventArgs e)
		{
			StartActivePerionDate = DateTime.Now.Date;
			EndActivePeriodDate = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
			FilterChanged?.Invoke();
		}

		protected void OnButtonTomorrowClicked(object sender, EventArgs e)
		{
			StartActivePerionDate = DateTime.Now.Date.AddDays(1);
			EndActivePeriodDate = DateTime.Now.Date.AddDays(1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
			FilterChanged?.Invoke();
		}

		protected void OnButtonThisWeekClicked(object sender, EventArgs e)
		{
			DateHelper.GetWeekPeriod(out StartActivePerionDate, out EndActivePeriodDate, 0);
			FilterChanged?.Invoke();
		}
	
		protected void OnButtonNextWeekClicked(object sender, EventArgs e)
		{
			DateHelper.GetWeekPeriod(out StartActivePerionDate, out EndActivePeriodDate, 1);
			FilterChanged?.Invoke();
		}

		protected void OnDateperiodpickerFilterPeriodChangedByUser(object sender, EventArgs e)
		{
			StartTaskCreateDate = null;
			EndTaskCreateDate = null;
			StartActivePerionDate = dateperiodpickerFilter.StartDateOrNull;
			EndActivePeriodDate = dateperiodpickerFilter.EndDateOrNull?.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
			FilterChanged?.Invoke();
		}

		protected void OnDateperiodpickerCompleteDateStartDateChanged(object sender, EventArgs e)
		{
			StartActivePerionDate = null;
			EndActivePeriodDate = null;
			StartTaskCreateDate = dateperiodpickerFilter.StartDateOrNull;
			EndTaskCreateDate = dateperiodpickerFilter.EndDateOrNull?.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
			FilterChanged?.Invoke();
		}

		protected void OnEntryreferencevmEmployeeChangedByUser(object sender, EventArgs e) => FilterChanged?.Invoke();

		protected void OnCheckbuttonHideCompletedToggled(object sender, EventArgs e) => FilterChanged?.Invoke();
	}
}
