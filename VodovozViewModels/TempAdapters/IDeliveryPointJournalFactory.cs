using System;
using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.Filters.ViewModels;
using Vodovoz.ViewModels.Journals.JournalViewModels.Client;

namespace Vodovoz.ViewModels.TempAdapters
{
	public interface IDeliveryPointJournalFactory
	{
		void SetDeliveryPointJournalFilterViewModel(Action<DeliveryPointJournalFilterViewModel>[] filterParams);
		IEntityAutocompleteSelectorFactory CreateDeliveryPointAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams);
		IEntityAutocompleteSelectorFactory CreateDeliveryPointByClientAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams);
		DeliveryPointJournalViewModel CreateDeliveryPointJournal(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams);
		DeliveryPointByClientJournalViewModel CreateDeliveryPointByClientJournal(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams);
	}
}
