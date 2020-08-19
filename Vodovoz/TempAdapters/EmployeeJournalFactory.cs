using QS.DomainModel.UoW;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Journals.Filters.Employees;

namespace Vodovoz.TempAdapters
{
    public class EmployeeJournalFactory : IEmployeeJournalFactory
    {
        public IEntityAutocompleteSelectorFactory CreateEmployeeAutocompleteSelectorFactory()
        {
            return new DefaultEntityAutocompleteSelectorFactory<Employee, EmployeesJournalViewModel,
                EmployeeFilterViewModel>(ServicesConfig.CommonServices);
        }

		public IEntityAutocompleteSelectorFactory CreateWorkingEmployeeAutocompleteSelectorFactory()
		{
			var selectorFactory = new EntitySelectorFactory<Employee, EmployeesJournalViewModel>(() => {
				var filter = new EmployeeFilterViewModel();
				filter.Status = EmployeeStatus.IsWorking;
				var journal = new EmployeesJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices);
				return journal;
			});

			return selectorFactory;
		}
	}
}