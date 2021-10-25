using Vodovoz.Domain.Employees;

namespace Vodovoz.ViewModels.TempAdapters
{
	public interface ICurrentUserSettings
	{
		UserSettings Settings { get; }
		void SaveSettings();
	}
}
