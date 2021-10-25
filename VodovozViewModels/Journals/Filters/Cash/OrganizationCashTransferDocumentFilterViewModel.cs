using System;
using System.Collections.Generic;
using Autofac;
using QS.Project.Filter;
using QS.ViewModels.Control.EEVM;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Organizations;
using Vodovoz.ViewModels.Journals.Filters.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ViewModels.Journals.FilterViewModels
{
    public class OrganizationCashTransferDocumentFilterViewModel : FilterViewModelBase<OrganizationCashTransferDocumentFilterViewModel>
    {
		public IEntityEntryViewModel WorkingEmployeeViewModel { get; private set; }
        public OrganizationCashTransferDocumentFilterViewModel(
			ILifetimeScope scope,
			DialogViewModelBase parrentDialog,
			params Action<OrganizationCashTransferDocumentFilterViewModel>[] filterParams)
        {
			if(scope == null)
			{
				throw new ArgumentNullException(nameof(scope));
			}
			if(parrentDialog == null)
			{
				throw new ArgumentNullException(nameof(parrentDialog));
			}
			Organizations = UoW.GetAll<Organization>();
			CreateEntryViewModel(scope, parrentDialog);
		}

		private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate; 
            set => UpdateFilterField(ref startDate, value);
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get => endDate; 
            set => UpdateFilterField(ref endDate, value);
        }

        private Employee author;
        public virtual Employee Author
        {
            get => author;
            set => UpdateFilterField(ref author, value);
        }

        public IEnumerable<Organization> Organizations { get; }

        private Organization organizationFrom;
        public virtual Organization OrganizationFrom
        {
            get => organizationFrom;
            set => UpdateFilterField(ref organizationFrom, value);
        }

        private Organization organizationTo;
        public virtual Organization OrganizationTo
        {
            get => organizationTo;
            set => UpdateFilterField(ref organizationTo, value);
        }
		
		private void CreateEntryViewModel(ILifetimeScope scope, DialogViewModelBase parrentDialog)
		{
			var builder = new CommonEEVMBuilderFactory<OrganizationCashTransferDocumentFilterViewModel>(
				parrentDialog, this, UoW, parrentDialog.NavigationManager);
			WorkingEmployeeViewModel = builder.ForProperty(x => x.Author)
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel, EmployeeFilterViewModel>(
					x => x.Status = EmployeeStatus.IsWorking)
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
		}
    }
}
