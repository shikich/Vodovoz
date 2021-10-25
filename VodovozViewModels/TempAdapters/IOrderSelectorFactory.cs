using System;
using Autofac;
using QS.Project.Journal.EntitySelector;
using Vodovoz.ViewModels.Journals.Filters.Orders;

namespace Vodovoz.TempAdapters
{
	public interface IOrderSelectorFactory
	{
		IEntitySelector CreateOrderSelectorForDocument(
			ILifetimeScope scope, params Action<OrderForMovDocJournalFilterViewModel>[] filterParams);

		IEntityAutocompleteSelectorFactory CreateOrderAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<OrderJournalFilterViewModel>[] filterParams);
	}
}
