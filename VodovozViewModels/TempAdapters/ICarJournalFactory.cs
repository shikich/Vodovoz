using System;
using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.ViewModels.Journals.Filters.Cars;

namespace Vodovoz.TempAdapters
{
    public interface ICarJournalFactory
    {
        IEntityAutocompleteSelectorFactory CreateCarAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<CarJournalFilterViewModel>[] filterParams);
    }
}
