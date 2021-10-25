using System;
using System.Linq;
using Autofac;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.Transform;
using QS.Deletion;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using QS.ViewModels.Control.EEVM;
using Vodovoz.Domain.Cash;
using Vodovoz.Domain.Employees;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.ViewModels.Journals.FilterViewModels;
using Vodovoz.ViewModels.Journals.JournalNodes;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Cash;
using Vodovoz.ViewModels.ViewModels.Employees;
using VodovozInfrastructure.Interfaces;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Cash
{
    public class CashRequestJournalViewModel: FilterableSingleEntityJournalViewModelBase
        <
            CashRequest,
            CashRequestViewModel,
            CashRequestJournalNode,
            CashRequestJournalFilterViewModel
        >
    {
		private readonly IEmployeeRepository employeeRepository;
        
        public CashRequestJournalViewModel(
            CashRequestJournalFilterViewModel filterViewModel,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommonServices commonServices,
            IEmployeeRepository employeeRepository,
			ILifetimeScope scope,
			INavigationManager navigationManager
        ) : base(unitOfWorkFactory, commonServices, filterViewModel, scope, navigationManager)
        {
			this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

			TabName = "Журнал заявок ДС";

			CreateBindingsForFilter();
			
            UpdateOnChanges(
                typeof(CashRequest),
                typeof(CashRequestSumItem),
                typeof(Subdivision),
                typeof(Employee)
            );
        }
		
		private void CreateBindingsForFilter()
		{
			var builder =
				new CommonEEVMBuilderFactory<CashRequestJournalFilterViewModel>(this, FilterViewModel, UoW, NavigationManager, Scope);

			var authorViewModel = builder.ForProperty(f => f.Author)
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel>()
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			var accountableEmployeeViewModel = builder.ForProperty(f => f.AccountableEmployee)
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel>()
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();

			FilterViewModel.SetEntriesViewModels(authorViewModel, accountableEmployeeViewModel);
		}

        protected override Func<IUnitOfWork, IQueryOver<CashRequest>> ItemsSourceQueryFunction => (uow) =>
        {
            CashRequest cashRequestAlias = null;
            Employee authorAlias = null;
            CashRequestSumItem cashRequestSumItemAlias = null;
            Employee accountableEmployeeAlias = null;

            CashRequestJournalNode resultAlias = null;

            var result = uow.Session.QueryOver<CashRequest>(() => cashRequestAlias)
                .Left.JoinAlias(с => с.Sums, () => cashRequestSumItemAlias)
                .Left.JoinAlias(с => с.Author, () => authorAlias)
                .Left.JoinAlias(() => cashRequestSumItemAlias.AccountableEmployee, () => accountableEmployeeAlias);
            
            if(FilterViewModel != null) {
                if(FilterViewModel.StartDate.HasValue)
                    result.Where(() => cashRequestAlias.Date >= FilterViewModel.StartDate.Value.Date);
                if(FilterViewModel.EndDate.HasValue)
                    result.Where(() => cashRequestAlias.Date < FilterViewModel.EndDate.Value.Date.AddDays(1));
                if(FilterViewModel.Author != null)
                    result.Where(() => authorAlias.Id == FilterViewModel.Author.Id);
                if (FilterViewModel.AccountableEmployee != null)
                    result.Where(() => accountableEmployeeAlias.Id == FilterViewModel.AccountableEmployee.Id);
                if (FilterViewModel.State != null )
                {
                    result.Where(() => cashRequestAlias.State == FilterViewModel.State);
                }
            }

            var userId = commonServices.UserService.CurrentUserId;
            var currentEmployee = employeeRepository.GetEmployeesForUser(uow, userId).First();
            var currentEmployeeId = currentEmployee.Id;

            if (!commonServices.UserService.GetCurrentUser(UoW).IsAdmin)
            {
                if (!commonServices.PermissionService
                        .ValidateUserPresetPermission("role_financier_cash_request", userId)
                    && !commonServices.PermissionService
                        .ValidateUserPresetPermission("role_coordinator_cash_request", userId)
                    && !commonServices.PermissionService
                        .ValidateUserPresetPermission("role_сashier", userId)
                    )
                {
                    if (commonServices.CurrentPermissionService.ValidatePresetPermission("can_see_current_subdivision_cash_requests"))
                    {
                        result.Where(() => authorAlias.Subdivision.Id == currentEmployee.Subdivision.Id);
                    }
                    else
                    {
                        result.Where(() => cashRequestAlias.Author.Id == currentEmployeeId);
                    }
                }

            }

            var authorProjection = Projections.SqlFunction(
                new SQLFunctionTemplate(NHibernateUtil.String, "CONCAT_WS(' ', ?1, ?2, ?3)"),
                NHibernateUtil.String,
                Projections.Property(() => authorAlias.LastName),
                Projections.Property(() => authorAlias.Name),
                Projections.Property(() => authorAlias.Patronymic)
            );
            
            var accauntableProjection = Projections.SqlFunction(
                new SQLFunctionTemplate(NHibernateUtil.String, "CONCAT_WS(' ', ?1, ?2, ?3)"),
                NHibernateUtil.String,
                Projections.Property(() => accountableEmployeeAlias.LastName),
                Projections.Property(() => accountableEmployeeAlias.Name),
                Projections.Property(() => accountableEmployeeAlias.Patronymic)
            );

            var cashReuestSumSubquery = QueryOver.Of(() => cashRequestSumItemAlias)
                .Where(() => cashRequestSumItemAlias.CashRequest.Id == cashRequestAlias.Id)
                .Where(Restrictions.IsNotNull(Projections.Property<CashRequestSumItem>(o => o.CashRequest)))
                .Select(Projections.Sum(() => cashRequestSumItemAlias.Sum));
            
            result.Where(GetSearchCriterion(
                () => authorAlias.Id,
                () => authorProjection,
                () => accauntableProjection,
                () => accountableEmployeeAlias.Id,
                () => cashRequestAlias.Basis
            ));
            
            result.SelectList(list => list
                    .SelectGroup(c => c.Id)
                    .Select(c => c.Id).WithAlias(() => resultAlias.Id)
                    .Select(c => c.Date).WithAlias(() => resultAlias.Date)
                    .Select(c => c.State).WithAlias(() => resultAlias.State)
                    .Select(c => c.DocumentType).WithAlias(() => resultAlias.DocumentType) 
                    .Select(authorProjection).WithAlias(() => resultAlias.Author)
                    .Select(accauntableProjection).WithAlias(() => resultAlias.AccountablePerson)
                    .SelectSubQuery(cashReuestSumSubquery).WithAlias(() => resultAlias.Sum)
                    .Select(c => c.Basis).WithAlias(() => resultAlias.Basis)
                ).TransformUsing(Transformers.AliasToBean<CashRequestJournalNode>())
                .OrderBy(x => x.Date).Desc();
            return result;
        };

        protected override void CreateNodeActions()
        {
            NodeActionsList.Clear();
            CreateDefaultSelectAction();
            CreateDefaultAddActions();
            CreateDefaultEditAction();
            CreateDeleteAction();
        }
        
        void CreateDeleteAction()
        {
            var deleteAction = new JournalAction("Удалить",
                (selected) => {
                    
                    var selectedNodes = selected.OfType<CashRequestJournalNode>();

                    if(selectedNodes == null || selectedNodes.Count() != 1) {
                        return false;
                    }

                    CashRequestJournalNode selectedNode = selectedNodes.First();

                    if (selectedNode.State != CashRequest.States.New){
                        return false;
                    }
                    
                    
                    if(!EntityConfigs.ContainsKey(selectedNode.EntityType)) {
                        return false;
                    }
                    
                    var config = EntityConfigs[selectedNode.EntityType];
                    return config.PermissionResult.CanDelete;
                },
                (selected) => true,
                (selected) => {
                    var selectedNodes = selected.OfType<CashRequestJournalNode>();
                    if(selectedNodes == null || selectedNodes.Count() != 1) {
                        return;
                    }
                    CashRequestJournalNode selectedNode = selectedNodes.First();
                    if(!EntityConfigs.ContainsKey(selectedNode.EntityType)) {
                        return;
                    }
                    var config = EntityConfigs[selectedNode.EntityType];
                    if(config.PermissionResult.CanDelete) {
                        DeleteHelper.DeleteEntity(selectedNode.EntityType, selectedNode.Id);
                    }
                },
               "Delete"
            );
            NodeActionsList.Add(deleteAction);
        }

		private CashRequestViewModel ResolveViewModel(TypedParameter entityUowBuilderParam)
		{
			var scope = Scope.BeginLifetimeScope();
			var fileChooser = scope.Resolve<IFileChooserProvider>(new TypedParameter(typeof(string), "Категории расхода.csv"));
			
			return scope.Resolve<CashRequestViewModel>(
				entityUowBuilderParam,
				new TypedParameter(typeof(IFileChooserProvider), fileChooser));
		}

        protected override Func<CashRequestViewModel> CreateDialogFunction => () =>
		{
			var entityUowBuilderParam = new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate());
			return ResolveViewModel(entityUowBuilderParam);

			/*return new CashRequestViewModel(
				EntityUoWBuilder.ForCreate(),
				unitOfWorkFactory,
				commonServices,
				fileChooserProvider,
				employeeRepository,
				cashRepository,
				_employeeJournalFactory,
				_subdivisionJournalFactory
			);*/
		};
        protected override Func<CashRequestJournalNode, CashRequestViewModel> OpenDialogFunction =>
            node =>
			{
				var entityUowBuilderParam = new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id));
				return ResolveViewModel(entityUowBuilderParam);
				
				/*return new CashRequestViewModel(
					EntityUoWBuilder.ForOpen(node.Id),
					unitOfWorkFactory,
					commonServices,
					fileChooserProvider,
					employeeRepository,
					cashRepository,
					_employeeJournalFactory,
					_subdivisionJournalFactory
				);*/
			};
    }
}
