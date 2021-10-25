using Autofac;
using QS.Project.Journal.EntitySelector;

namespace Vodovoz.ViewModels.Journals.JournalFactories
{
	public interface ISubdivisionJournalFactory
	{
		IEntityAutocompleteSelectorFactory CreateSubdivisionAutocompleteSelectorFactory(ILifetimeScope scope);
		IEntityAutocompleteSelectorFactory CreateDefaultSubdivisionAutocompleteSelectorFactory(ILifetimeScope scope);
		IEntityAutocompleteSelectorFactory CreateLogisticSubdivisionAutocompleteSelectorFactory(ILifetimeScope scope);
	}
}
