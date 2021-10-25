using System;
using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.Domain.Logistic;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Journals.Filters.Cars;

namespace Vodovoz.TempAdapters
{
    public class CarJournalFactory : ICarJournalFactory
    {
        public IEntityAutocompleteSelectorFactory CreateCarAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<CarJournalFilterViewModel>[] filterParams)
		{
			var newScope = scope.BeginLifetimeScope();
            return new EntityAutocompleteSelectorFactory<CarJournalViewModel>(
				typeof(Car),
				() => ResolveJournal(newScope, filterParams));
        }

		private CarJournalViewModel ResolveJournal(ILifetimeScope scope, params Action<CarJournalFilterViewModel>[] filterParams)
		{
			if(filterParams != null)
			{
				return scope.Resolve<CarJournalViewModel>(new TypedParameter(
					typeof(Action<CarJournalFilterViewModel>[]), filterParams));
			}

			return scope.Resolve<CarJournalViewModel>();
		}
    }
}
