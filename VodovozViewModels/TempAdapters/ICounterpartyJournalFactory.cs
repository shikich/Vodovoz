using QS.Project.Journal;

namespace Vodovoz.TempAdapters
{
    public interface ICounterpartyJournalFactory
    {
        JournalViewModelBase CreateDefaultCounterpartyJournal();
    }
}