using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;

namespace Vodovoz.TempAdapters
{
    public class CounterpartyJournalFactory : ICounterpartyJournalFactory
    {
        public IEntityAutocompleteSelectorFactory CreateCounterpartyAutocompleteSelectorFactory() => 
            new DefaultEntityAutocompleteSelectorFactory<Counterparty, 
                CounterpartyJournalViewModel, CounterpartyJournalFilterViewModel>(ServicesConfig.CommonServices);
    }
}