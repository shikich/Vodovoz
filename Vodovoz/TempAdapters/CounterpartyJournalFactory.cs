using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.Domain.Client;
using Vodovoz.JournalViewModels;

namespace Vodovoz.TempAdapters
{
    public class CounterpartyJournalFactory : ICounterpartyJournalFactory
    {
        public IEntityAutocompleteSelectorFactory CreateCounterpartyAutocompleteSelectorFactory(ILifetimeScope scope) =>
			new EntityAutocompleteSelectorFactory<CounterpartyJournalViewModel>(
				typeof(Counterparty),
				() =>
				{
					var newScope = scope.BeginLifetimeScope();
					return newScope.Resolve<CounterpartyJournalViewModel>();
				});
	}
}
