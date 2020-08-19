using System;
using QS.DomainModel.Entity;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.WageCalculation;
using QS.Project.Filter;

namespace Vodovoz.ViewModels.Journals.Filters.Employees
{
	public class EmployeeFilterViewModel : FilterViewModelBase<EmployeeFilterViewModel>
	{
		private EmployeeCategory? category;
		public virtual EmployeeCategory? Category {
			get => category;
			set => UpdateFilterField(ref category, value);
		}

		private EmployeeCategory? restrictCategory;
		[PropertyChangedAlso(nameof(IsCategoryNotRestricted))]
		public virtual EmployeeCategory? RestrictCategory {
			get => restrictCategory;
			set {
				if(SetField(ref restrictCategory, value)) {
					Category = RestrictCategory;
					Update();
				}
			}
		}

		public bool IsCategoryNotRestricted => !RestrictCategory.HasValue;

		EmployeeStatus? status;
		public virtual EmployeeStatus? Status {
			get => status;
			set => UpdateFilterField(ref status, value);
		}

		private bool canChangeStatus = true;
		public bool CanChangeStatus {
			get => canChangeStatus; 
			set => UpdateFilterField(ref canChangeStatus, value); 
		}

		private DateTime? weekDay;
		public virtual DateTime? WeekDay {
			get => weekDay;
			set => UpdateFilterField(ref weekDay, value);
		}

		private TimeSpan? drvStartTime;
		public virtual TimeSpan? DrvStartTime {
			get => drvStartTime;
			set => UpdateFilterField(ref drvStartTime, value);
		}

		private TimeSpan? drvEndTime;
		public virtual TimeSpan? DrvEndTime {
			get => drvEndTime;
			set => UpdateFilterField(ref drvEndTime, value);
		}

		WageParameterItemTypes? restrictWageParameterItemType;
		public virtual WageParameterItemTypes? RestrictWageParameterItemType {
			get => restrictWageParameterItemType;
			set => UpdateFilterField(ref restrictWageParameterItemType, value);
		}
	}
}