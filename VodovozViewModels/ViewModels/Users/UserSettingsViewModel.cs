using System;
using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Employees;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Services;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.JournalFactories;

namespace Vodovoz.ViewModels.Users
{
	public class UserSettingsViewModel : EntityTabViewModelBase<UserSettings>
	{
		private readonly IEmployeeService _employeeService;
		private readonly ISubdivisionService _subdivisionService;

		public UserSettingsViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			IEmployeeService employeeService,
			ISubdivisionService subdivisionService,
			ISubdivisionJournalFactory subdivisionJournalFactory,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			ILifetimeScope scope,
			INavigationManager navigationManager = null)
			: base(uowBuilder, unitOfWorkFactory, commonServices, navigationManager, scope)
		{
			_employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService)); ;
			_subdivisionService = subdivisionService ?? throw new ArgumentNullException(nameof(subdivisionService));
			SubdivisionAutocompleteSelectorFactory =
				(subdivisionJournalFactory ?? throw new ArgumentNullException(nameof(subdivisionJournalFactory)))
				.CreateDefaultSubdivisionAutocompleteSelectorFactory(Scope);
			CounterpartyAutocompleteSelectorFactory =
				(counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory)))
				.CreateCounterpartyAutocompleteSelectorFactory(Scope);
		}

		public IEntityAutocompleteSelectorFactory SubdivisionAutocompleteSelectorFactory { get; }
		public IEntityAutocompleteSelectorFactory CounterpartyAutocompleteSelectorFactory { get; }
		
		public bool IsUserFromOkk => _subdivisionService.GetOkkId() == _employeeService.GetEmployeeForUser(UoW, CommonServices.UserService.CurrentUserId)?.Subdivision?.Id;

		public bool IsUserFromRetail => CommonServices.CurrentPermissionService.ValidatePresetPermission("user_have_access_to_retail");
	}
}
