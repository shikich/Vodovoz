using QSOrmProject.Users;
using Vodovoz.Domain.Employees;
using Vodovoz.EntityRepositories;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz
{
	public class CurrentUserSettings : ICurrentUserSettings
	{
		private readonly UserSettingsManager<UserSettings> _manager;
		private readonly IUserRepository _userRepository = new UserRepository();

		public CurrentUserSettings()
		{
			_manager = new UserSettingsManager<UserSettings>
			{
				CreateUserSettings = uow => new UserSettings(_userRepository.GetCurrentUser(uow)),
				LoadUserSettings = _userRepository.GetCurrentUserSettings
			};
		}

		public UserSettings Settings => _manager.Settings;

		public void SaveSettings() => _manager.SaveSettings();
	}
}

