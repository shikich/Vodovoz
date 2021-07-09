using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;

namespace Vodovoz.ViewModels.Journals.JournalSelectors
{
    public class EmployeePostsJournalFactory : IEmployeePostsJournalFactory
    {
        public IEntityAutocompleteSelectorFactory CreateEmployeePostsAutoCompleteSelectorFactory(bool multipleSelect = false)
        {
	        return new EntityAutocompleteSelectorFactory<EmployeePostsJournalViewModel>(
		        typeof(Employee),
		        () => new EmployeePostsJournalViewModel(UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices)
		        {
			        SelectionMode = multipleSelect ? JournalSelectionMode.Multiple : JournalSelectionMode.Single
		        });
        }
    }
}