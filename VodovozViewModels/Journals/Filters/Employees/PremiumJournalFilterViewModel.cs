using QS.Project.Filter;
using System;
using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.FilterViewModels.Organization;
using Vodovoz.Journals.JournalViewModels.Organization;
using Vodovoz.ViewModels.Journals.JournalFactories;

namespace Vodovoz.ViewModels.Journals.FilterViewModels.Employees
{
	public class PremiumJournalFilterViewModel : FilterViewModelBase<PremiumJournalFilterViewModel>
	{
		private DateTime? _startDate;
		private DateTime? _endDate;
		private Subdivision _subdivision;

		public PremiumJournalFilterViewModel(
			ILifetimeScope scope,
			ISubdivisionJournalFactory subdivisionJournalFactory,
			params Action<PremiumJournalFilterViewModel>[] filterParams)
		{
			if(scope == null)
			{
				throw new ArgumentNullException(nameof(scope));
			}
			if(subdivisionJournalFactory == null)
			{
				throw new ArgumentNullException(nameof(subdivisionJournalFactory));
			}

			SubdivisionAutocompleteSelectorFactory = subdivisionJournalFactory.CreateDefaultSubdivisionAutocompleteSelectorFactory(scope);

			if(filterParams != null)
			{
				SetAndRefilterAtOnce(filterParams);
			}
		}

		public IEntityAutocompleteSelectorFactory SubdivisionAutocompleteSelectorFactory { get; }

		public virtual DateTime? StartDate
		{
			get => _startDate;
			set => UpdateFilterField(ref _startDate, value);
		}

		public virtual DateTime? EndDate
		{
			get => _endDate;
			set => UpdateFilterField(ref _endDate, value);
		}

		public virtual Subdivision Subdivision
		{
			get => _subdivision;
			set => UpdateFilterField(ref _subdivision, value);
		}
	}
}
