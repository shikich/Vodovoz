using System;
using System.Collections;
using Autofac;
using QS.DomainModel.UoW;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Factories;
using Vodovoz.Filters.ViewModels;
using Vodovoz.ViewModels.Journals.JournalViewModels.Client;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.TempAdapters
{
	public class DeliveryPointJournalFactory : IDeliveryPointJournalFactory
	{
		private Action<DeliveryPointJournalFilterViewModel>[] _filterParams;

		public void SetDeliveryPointJournalFilterViewModel(Action<DeliveryPointJournalFilterViewModel>[] filterParams)
		{
			_filterParams = filterParams;
		}

		public IEntityAutocompleteSelectorFactory CreateDeliveryPointAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams)
		{
			return new EntityAutocompleteSelectorFactory<DeliveryPointJournalViewModel>(
				typeof(DeliveryPoint),
				() => CreateDeliveryPointJournal(scope, filterParams));
		}

		public IEntityAutocompleteSelectorFactory CreateDeliveryPointByClientAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams)
		{
			return new EntityAutocompleteSelectorFactory<DeliveryPointByClientJournalViewModel>(
				typeof(DeliveryPoint),
				() => CreateDeliveryPointByClientJournal(scope, filterParams));
		}

		public DeliveryPointJournalViewModel CreateDeliveryPointJournal(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams)
		{
			var newScope = scope.BeginLifetimeScope();
			return newScope.Resolve<DeliveryPointJournalViewModel>(
				new TypedParameter(typeof(bool), true),
				new TypedParameter(typeof(bool), true),
				new TypedParameter(typeof(Action<DeliveryPointJournalFilterViewModel>[]), filterParams));
			
			/*var journal = new DeliveryPointJournalViewModel(
				_deliveryPointViewModelFactory,
				_deliveryPointJournalFilter ?? new DeliveryPointJournalFilterViewModel(),
				UnitOfWorkFactory.GetDefaultFactory,
				ServicesConfig.CommonServices,
				hideJournalForOpen: true,
				hideJournalForCreate: true);
			
			return journal;*/
		}

		public DeliveryPointByClientJournalViewModel CreateDeliveryPointByClientJournal(
			ILifetimeScope scope, params Action<DeliveryPointJournalFilterViewModel>[] filterParams)
		{
			var newScope = scope.BeginLifetimeScope();
			
			return newScope.Resolve<DeliveryPointByClientJournalViewModel>(
				new TypedParameter(typeof(bool), true),
				new TypedParameter(typeof(bool), true),
				new TypedParameter(typeof(Action<DeliveryPointJournalFilterViewModel>[]), filterParams ?? _filterParams));
			
			/*var journal = new DeliveryPointByClientJournalViewModel(
				_deliveryPointViewModelFactory,
				_deliveryPointJournalFilter
				?? throw new ArgumentNullException($"Ожидался фильтр {nameof(_deliveryPointJournalFilter)} с указанным клиентом"),
				UnitOfWorkFactory.GetDefaultFactory,
				ServicesConfig.CommonServices,
				hideJournalForOpen: true,
				hideJournalForCreate: true);
			
			return journal;*/
		}
	}
}
