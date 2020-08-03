using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;

namespace Vodovoz.TempAdapters
{
    public class EmployeeJournalFactory : IEmployeeJournalFactory
    {
        public JournalViewModelBase CreateWorkingEmployeeJournal()
        {
            var employeeFilter = new EmployeeFilterViewModel();
            employeeFilter.SetAndRefilterAtOnce(x => x.Status = EmployeeStatus.IsWorking);
            var employeesJournal = new EmployeesJournalViewModel(employeeFilter, UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices);
            return employeesJournal;
        }
    }
}