using QS.Project.Journal;

namespace Vodovoz.TempAdapters
{
    public interface IEmployeeJournalFactory
    {
        JournalViewModelBase CreateWorkingEmployeeJournal();
    }
}