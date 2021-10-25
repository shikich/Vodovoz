using System;
using System.Collections;
using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.FilterViewModels.Organization;
using Vodovoz.Journals.JournalViewModels.Organization;
using Vodovoz.ViewModels.Journals.JournalFactories;

namespace Vodovoz.TempAdapters
{
	public class SubdivisionJournalFactory : ISubdivisionJournalFactory
	{
		public IEntityAutocompleteSelectorFactory CreateSubdivisionAutocompleteSelectorFactory(ILifetimeScope scope)
		{
			return new EntityAutocompleteSelectorFactory<SubdivisionsJournalViewModel>(
				typeof(Subdivision),
				() => ResolveJournal(scope.BeginLifetimeScope()));
		}
		
		public IEntityAutocompleteSelectorFactory CreateDefaultSubdivisionAutocompleteSelectorFactory(
			ILifetimeScope scope)
		{
			return new EntityAutocompleteSelectorFactory<SubdivisionsJournalViewModel>(typeof(Subdivision),
				() => ResolveJournal(
					scope.BeginLifetimeScope(),
					new Action<SubdivisionFilterViewModel>[]
					{
						x => x.SubdivisionType = SubdivisionType.Default	
					}));
		}

		public IEntityAutocompleteSelectorFactory CreateLogisticSubdivisionAutocompleteSelectorFactory(ILifetimeScope scope)
		{
			return new EntityAutocompleteSelectorFactory<SubdivisionsJournalViewModel>(
				typeof(Subdivision),
				() => ResolveJournal(
					scope.BeginLifetimeScope(),
					new Action<SubdivisionFilterViewModel>[]
					{
						x => x.SubdivisionType = SubdivisionType.Logistic	
					}));
		}

		private SubdivisionsJournalViewModel ResolveJournal(ILifetimeScope scope, IEnumerable filterParams = null) =>
			filterParams == null
				? scope.Resolve<SubdivisionsJournalViewModel>()
				: scope.Resolve<SubdivisionsJournalViewModel>(
					new TypedParameter(typeof(Action<SubdivisionFilterViewModel>[]), filterParams));
	}
}
