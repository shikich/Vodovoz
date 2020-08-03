using QS.Project.Journal;

namespace Vodovoz.TempAdapters
{
    public interface ICarJournalFactory
    {
        JournalViewModelBase CreateDefaultCarsJournal();
    }
}