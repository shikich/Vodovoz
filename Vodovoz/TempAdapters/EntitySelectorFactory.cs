using System;
using QS.DomainModel.Entity;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;

namespace Vodovoz.TempAdapters
{
	public class EntitySelectorFactory<TEntity, TJournal> : IEntityAutocompleteSelectorFactory
		where TJournal: JournalViewModelBase, IEntityAutocompleteSelector
		where TEntity : class, IDomainObject
	{
		private readonly Func<TJournal> selectorFunc;

		public EntitySelectorFactory(Func<TJournal> selectorFunc)
		{
			this.selectorFunc = selectorFunc ?? throw new ArgumentNullException(nameof(selectorFunc));
		}

		public Type EntityType => typeof(TEntity);

		public IEntityAutocompleteSelector CreateAutocompleteSelector(bool multipleSelect = false)
		{
			var selector = selectorFunc.Invoke();
			if(multipleSelect) {
				selector.SelectionMode = JournalSelectionMode.Multiple;
			} else {
				selector.SelectionMode = JournalSelectionMode.Single;
			}
			return selector;
		}

		public IEntitySelector CreateSelector(bool multipleSelect = false)
		{
			return CreateAutocompleteSelector(multipleSelect);
		}
	}
}
