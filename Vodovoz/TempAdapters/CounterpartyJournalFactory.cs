using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Services;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;

namespace Vodovoz.TempAdapters
{
    public class CounterpartyJournalFactory : ICounterpartyJournalFactory
    {
        public JournalViewModelBase CreateDefaultCounterpartyJournal()
        {
            CounterpartyJournalFilterViewModel filter = new CounterpartyJournalFilterViewModel();
            var counterpartyJournal = new CounterpartyJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices);
            return counterpartyJournal;
        }
    }
}