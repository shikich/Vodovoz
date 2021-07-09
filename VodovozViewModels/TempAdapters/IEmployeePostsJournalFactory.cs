using QS.Project.Journal.EntitySelector;

namespace Vodovoz.TempAdapters
{
	public interface IEmployeePostsJournalFactory
	{
		IEntityAutocompleteSelectorFactory CreateEmployeePostsAutoCompleteSelectorFactory(bool multipleSelect = false);
	}
}