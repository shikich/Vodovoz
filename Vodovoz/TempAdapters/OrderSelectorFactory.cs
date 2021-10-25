using System;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using Autofac;
using Vodovoz.Domain.Orders;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Journals.Filters.Orders;

namespace Vodovoz.TempAdapters
{
	public class OrderSelectorFactory : IOrderSelectorFactory
	{
		public IEntitySelector CreateOrderSelectorForDocument(
			ILifetimeScope scope, params Action<OrderForMovDocJournalFilterViewModel>[] filterParams)
		{
			var newScope = scope.BeginLifetimeScope();
			OrderJournalViewModel journal;
			
			if(filterParams != null)
			{
				journal = newScope.Resolve<OrderJournalViewModel>(
					new TypedParameter(typeof(Action<OrderJournalFilterViewModel>[]), filterParams));
			}
			else
			{
				journal = newScope.Resolve<OrderJournalViewModel>();
			}

			journal.SelectionMode = JournalSelectionMode.Multiple;
			
			return journal;
		}

		public IEntityAutocompleteSelectorFactory CreateOrderAutocompleteSelectorFactory(
			ILifetimeScope scope, params Action<OrderJournalFilterViewModel>[] filterParams)
		{
			return new EntityAutocompleteSelectorFactory<OrderJournalViewModel>(typeof(Order),
				() =>
				{
					var newScope = scope.BeginLifetimeScope();

					if(filterParams != null)
					{
						return newScope.Resolve<OrderJournalViewModel>(
							new TypedParameter(typeof(Action<OrderJournalFilterViewModel>[]), filterParams));
					}

					return newScope.Resolve<OrderJournalViewModel>();
				}
			);
		}
	}
}
