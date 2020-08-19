using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.Commands;
using QS.DomainModel.Entity.EntityPermissions.EntityExtendedPermission;
using QS.DomainModel.NotifyChange;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Cash;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.Infrastructure.Services;
using Vodovoz.PermissionExtensions;
using Vodovoz.TempAdapters;
using VodovozInfrastructure.Extensions;

namespace Vodovoz.ViewModels.Dialogs.Cash
{
	public class CashIncomeViewModel : EntityTabViewModelBase<Income>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		private readonly IEmployeeService employeeService;
		private readonly IEntityExtendedPermissionValidator entityExtendedPermissionValidator;
		private readonly IReportViewOpener reportViewOpener;
		private readonly IEntityChangeWatcher entityChangeWatcher;
		private readonly ICashCategoryRepository cashCategoryRepository;
		private readonly IEmployeeJournalFactory employeeJournalFactory;
		private readonly ICounterpartyJournalFactory counterpartyJournalFactory;
		private readonly IPermissionResult permissionResult;
		private readonly IInteractiveService interactiveService;
		private readonly IUserService userService;
		private readonly bool canEditRectroactively;

		public CashIncomeViewModel(
			IEntityUoWBuilder uowBuilder, 
			IUnitOfWorkFactory unitOfWorkFactory,
			IEmployeeService employeeService,
			IEntityExtendedPermissionValidator entityExtendedPermissionValidator,
			IReportViewOpener reportViewOpener,
			ICommonServices commonServices,
			IEntityChangeWatcher entityChangeWatcher,
			ICashCategoryRepository cashCategoryRepository,
			IEmployeeJournalFactory employeeJournalFactory,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			INavigationManager navigation = null)
		: base(uowBuilder, unitOfWorkFactory, commonServices, navigation)
		{
			this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
			this.entityExtendedPermissionValidator = entityExtendedPermissionValidator ?? throw new ArgumentNullException(nameof(entityExtendedPermissionValidator));
			this.reportViewOpener = reportViewOpener ?? throw new ArgumentNullException(nameof(reportViewOpener));
			this.entityChangeWatcher = entityChangeWatcher ?? throw new ArgumentNullException(nameof(entityChangeWatcher));
			this.cashCategoryRepository = cashCategoryRepository ?? throw new ArgumentNullException(nameof(cashCategoryRepository));
			this.employeeJournalFactory = employeeJournalFactory ?? throw new ArgumentNullException(nameof(employeeJournalFactory));
			this.counterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));
			this.interactiveService = commonServices.InteractiveService;
			this.userService = commonServices.UserService;

			Entity.Casher = employeeService.GetEmployeeForCurrentUser(UoW);
			if(Entity.Casher == null) {
				interactiveService.ShowMessage(
					QS.Dialog.ImportanceLevel.Error, 
					"Ваш пользователь не привязан к действующему сотруднику, " +
					"вы не можете создавать кассовые документы, " +
					"так как некого указывать в качестве кассира."
				);
				FailInitialize = true;
				return;
			}

			permissionResult = commonServices.PermissionService.ValidateUserPermission(typeof(Income), userService.CurrentUserId);
			if(uowBuilder.IsNewEntity) {
				if(!permissionResult.CanCreate) {
					interactiveService.ShowMessage(
						QS.Dialog.ImportanceLevel.Error,
						"Отсутствуют права на создание приходного ордера"
					);
					FailInitialize = true;
					return;
				}
			} else {
				if(!permissionResult.CanRead) {
					interactiveService.ShowMessage(
						QS.Dialog.ImportanceLevel.Error,
						"Отсутствуют права на просмотр приходного ордера"
					);
					FailInitialize = true;
					return;
				}

				canEditRectroactively = entityExtendedPermissionValidator.Validate(
					typeof(Income),
					userService.CurrentUserId, 
					new RetroactivelyClosePermission()
				);
			}

			// Настроить виджет доступных подразделений кассы
			//if(!accessfilteredsubdivisionselectorwidget.Configure(UoW, false, typeof(Income))) {
			//	MessageDialogHelper.RunErrorDialog(accessfilteredsubdivisionselectorwidget.ValidationErrorMessage);
			//	FailInitialize = true;
			//	return;
			//}

			if(uowBuilder.IsNewEntity) {
				Entity.Date = DateTime.Now;
			}

			ConfigureDlg();
		}

		private void ConfigureDlg()
		{
			entityChangeWatcher.BatchSubscribeOnEntity<IncomeCategory>((args) => UpdateIncomeCategories());
			entityChangeWatcher.BatchSubscribeOnEntity<ExpenseCategory>((args) => UpdateExpenseCategories());
			UpdateIncomeCategories();
			UpdateExpenseCategories();
		}

		public bool CanEdit => (UoW.IsNew && permissionResult.CanCreate) ||
								(permissionResult.CanUpdate && Entity.Date.Date == DateTime.Now.Date) ||
								canEditRectroactively;

		public bool IsPayment => Entity.TypeOperation == IncomeType.Payment;
		public bool IsDriverReport => Entity.TypeOperation == IncomeType.DriverReport;
		public bool IsDebtReturn => Entity.TypeOperation == IncomeType.Return;
		public bool IsCommonIncome => Entity.TypeOperation == IncomeType.Common;


		#region Cash categories

		private IList<IncomeCategory> incomeCategories;
		public virtual IList<IncomeCategory> IncomeCategories {
			get => incomeCategories;
			set => SetField(ref incomeCategories, value);
		}

		private void UpdateIncomeCategories()
		{
			IncomeCategories = cashCategoryRepository.IncomeCategories(UoW);
		}

		private IList<ExpenseCategory> expenseCategories;
		public virtual IList<ExpenseCategory> ExpenseCategories {
			get => expenseCategories;
			set => SetField(ref expenseCategories, value);
		}

		private void UpdateExpenseCategories()
		{
			ExpenseCategories = cashCategoryRepository.ExpenseCategories(UoW);
		}

		#endregion Cash categories

		#region Selectors

		private IEntityAutocompleteSelectorFactory employeesSelectorFactory;
		public IEntityAutocompleteSelectorFactory EmployeesSelectorFactory {
			get {
				if(employeesSelectorFactory == null) {
					employeesSelectorFactory = employeeJournalFactory.CreateWorkingEmployeeAutocompleteSelectorFactory();
				}
				return employeesSelectorFactory;
			}
		}

		private IEntityAutocompleteSelectorFactory counterpartySelectorFactory;
		public IEntityAutocompleteSelectorFactory CounterpartySelectorFactory {
			get {
				if(counterpartySelectorFactory == null) {
					counterpartySelectorFactory = counterpartyJournalFactory.CreateCounterpartyAutocompleteSelectorFactory();
				}
				return counterpartySelectorFactory;
			}
		}

		#endregion Selectors

		#region Commands

		#region PrintCommand

		private DelegateCommand printCommand;
		public DelegateCommand PrintCommand {
			get {
				if(printCommand == null) {
					printCommand = new DelegateCommand(
						Print,
						() => IsDebtReturn
					);
					printCommand.CanExecuteChangedWith(this, x => x.IsDebtReturn);
				}
				return printCommand;
			}
		}

		private void Print()
		{
			if(UoW.HasChanges && interactiveService.Question($"Перед печатью необходимо сохранить документ. Сохранить?", "Сохранение")) {
				Save();
			}

			var reportInfo = new QS.Report.ReportInfo {
				Title = String.Format("Квитанция №{0} от {1:d}", Entity.Id, Entity.Date),
				Identifier = "Cash.ReturnTicket",
				Parameters = new Dictionary<string, object> {
					{ "id",  Entity.Id }
				}
			};
			reportViewOpener.OpenReport(this, reportInfo);
		}

		#endregion PrintCommand

		#endregion Commands

		#region Payment

		#endregion Payment

		#region Income from route list

		#endregion Income from route list



		#region Other

		#endregion Other
	}
}
