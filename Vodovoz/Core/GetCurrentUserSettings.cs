using Vodovoz.Domain.Employees;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.Core
{
    public class GetCurrentUserSettings : ICurrentUserSettings
    {
        public UserSettings GetUserSettings() => CurrentUserSettings.Settings;
    }
}