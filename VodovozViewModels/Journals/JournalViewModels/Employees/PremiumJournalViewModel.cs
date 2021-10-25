using NHibernate;
using NHibernate.Transform;
using QS.Deletion;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using System;
using System.Linq;
using Autofac;
using QS.Navigation;
using QS.Project.Journal.DataLoader;
using Vodovoz.Domain.Employees;
using Vodovoz.ViewModels.Journals.FilterViewModels.Employees;
using Vodovoz.ViewModels.Journals.JournalNodes;
using Vodovoz.ViewModels.ViewModels.Employees;
using QS.Project.DB;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Employees
{
	public class PremiumJournalViewModel : FilterableMultipleEntityJournalViewModelBase<PremiumJournalNode, PremiumJournalFilterViewModel>
	{
		private readonly ICommonServices _commonServices;
		
		public PremiumJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			INavigationManager navigationManager,
			ILifetimeScope scope,
			params Action<PremiumJournalFilterViewModel>[] filterParams)
			: base(unitOfWorkFactory, commonServices, navigationManager, scope, filterParams)
		{
			_commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));

			TabName = "Журнал премий";

			RegisterPremiums();
			RegisterPremiumsRaskatGAZelle();

			var threadLoader = DataLoader as ThreadDataLoader<PremiumJournalNode>;
			threadLoader.MergeInOrderBy(x => x.Id, true);

			FinishJournalConfiguration();

			UpdateOnChanges(
				typeof(Premium),
				typeof(PremiumRaskatGAZelle),
				typeof(PremiumItem)
			);
		}

		protected override void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultSelectAction();
			CreateDefaultAddActions();
			CreateDefaultEditAction();
			CreateCustomDeleteAction();
		}

		private void RegisterPremiums()
		{
			var premiumsConfig = RegisterEntity<Premium>(GetPremiumsQuery)
				.AddDocumentConfiguration(
					//функция диалога создания документа
					() =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<PremiumViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
						
						/*return new PremiumViewModel(
							EntityUoWBuilder.ForCreate(),
							UnitOfWorkFactory,
							_commonServices,
							_employeeService,
							_premiumTemplateJournalFactory
						);*/
					},
					//функция диалога открытия документа
					(PremiumJournalNode node) =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<PremiumViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
						
						/*return new PremiumViewModel(
							EntityUoWBuilder.ForOpen(node.Id),
							UnitOfWorkFactory,
							_commonServices,
							_employeeService,
							_premiumTemplateJournalFactory
						);*/
					},
					//функция идентификации документа 
					(PremiumJournalNode node) => node.EntityType == typeof(Premium),
					"Премия",
					new JournalParametersForDocument { HideJournalForCreateDialog = false, HideJournalForOpenDialog = false }
				);

			//завершение конфигурации
			premiumsConfig.FinishConfiguration();
		}

		private void RegisterPremiumsRaskatGAZelle()
		{
			var premiumRaskatGAZelleConfig =
				RegisterEntity<PremiumRaskatGAZelle>(GetPremiumsRaskatGAZelleQuery)
				.AddDocumentConfigurationWithoutCreation(
				//функция диалога открытия документа
				(PremiumJournalNode node) => new PremiumRaskatGAZelleViewModel(
					EntityUoWBuilder.ForOpen(node.Id),
					UnitOfWorkFactory,
					_commonServices
				),
				//функция идентификации документа 
				(PremiumJournalNode node) => node.EntityType == typeof(PremiumRaskatGAZelle),
				new JournalParametersForDocument { HideJournalForCreateDialog = false, HideJournalForOpenDialog = false }
				);

			//завершение конфигурации
			premiumRaskatGAZelleConfig.FinishConfiguration();
		}

		private IQueryOver<Premium> GetPremiumsQuery(IUnitOfWork uow)
		{
			PremiumJournalNode resultAlias = null;
			Premium premiumAlias = null;
			PremiumItem premiumItemAlias = null;
			Employee employeeAlias = null;

			var query = uow.Session.QueryOver<Premium>(() => premiumAlias)
				.JoinAlias(f => f.Items, () => premiumItemAlias)
				.JoinAlias(() => premiumItemAlias.Employee, () => employeeAlias);

			if(FilterViewModel.Subdivision != null)
			{
				query.Where(() => employeeAlias.Subdivision.Id == FilterViewModel.Subdivision.Id);
			}

			if(FilterViewModel.StartDate.HasValue)
			{
				query.Where(() => premiumAlias.Date >= FilterViewModel.StartDate.Value);
			}

			if(FilterViewModel.EndDate.HasValue)
			{
				query.Where(() => premiumAlias.Date <= FilterViewModel.EndDate.Value);
			}

			var employeeProjection = CustomProjections.Concat_WS(
				" ",
				() => employeeAlias.LastName,
				() => employeeAlias.Name,
				() => employeeAlias.Patronymic
			);

			query.Where(
					GetSearchCriterion(
						() => premiumAlias.Id,
						() => premiumAlias.PremiumReasonString,
						() => employeeProjection,
						() => premiumAlias.TotalMoney
				)
			);

			var resultQuery = query
				.SelectList(list => list
					.SelectGroup(() => premiumAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => premiumAlias.Date).WithAlias(() => resultAlias.Date)
					.Select(CustomProjections.GroupConcat(
						employeeProjection, 
						false,
						employeeProjection,
						OrderByDirection.Asc,
						"\n")
					).WithAlias(() => resultAlias.EmployeesName)
					.Select(() => premiumAlias.PremiumReasonString).WithAlias(() => resultAlias.PremiumReason)
					.Select(() => premiumAlias.TotalMoney).WithAlias(() => resultAlias.PremiumSum)
				).OrderBy(o => o.Date).Desc
			.TransformUsing(Transformers.AliasToBean<PremiumJournalNode<Premium>>());

			return resultQuery;
		}

		private IQueryOver<PremiumRaskatGAZelle> GetPremiumsRaskatGAZelleQuery(IUnitOfWork uow)
		{
			PremiumJournalNode resultAlias = null;
			PremiumRaskatGAZelle premiumRaskatGAZelleAlias = null;
			PremiumItem premiumItemAlias = null;
			Employee employeeAlias = null;

			var query = uow.Session.QueryOver<PremiumRaskatGAZelle>(() => premiumRaskatGAZelleAlias)
				.JoinAlias(f => f.Items, () => premiumItemAlias)
				.JoinAlias(() => premiumItemAlias.Employee, () => employeeAlias);

			if(FilterViewModel.Subdivision != null)
			{
				query.Where(() => employeeAlias.Subdivision.Id == FilterViewModel.Subdivision.Id);
			}

			if(FilterViewModel.StartDate.HasValue)
			{
				query.Where(() => premiumRaskatGAZelleAlias.Date >= FilterViewModel.StartDate.Value);
			}

			if(FilterViewModel.EndDate.HasValue)
			{
				query.Where(() => premiumRaskatGAZelleAlias.Date <= FilterViewModel.EndDate.Value);
			}

			var employeeProjection = CustomProjections.Concat_WS(
				" ",
				() => employeeAlias.LastName,
				() => employeeAlias.Name,
				() => employeeAlias.Patronymic
			);

			query.Where(
				GetSearchCriterion(
					() => premiumRaskatGAZelleAlias.Id,
					() => premiumRaskatGAZelleAlias.PremiumReasonString,
					() => employeeProjection,
					() => premiumRaskatGAZelleAlias.TotalMoney
				)
			);

			var resultQuery = query
				.SelectList(list => list
					.Select(() => premiumRaskatGAZelleAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => premiumRaskatGAZelleAlias.Date).WithAlias(() => resultAlias.Date)
					.Select(employeeProjection).WithAlias(() => resultAlias.EmployeesName)
					.Select(() => premiumRaskatGAZelleAlias.PremiumReasonString).WithAlias(() => resultAlias.PremiumReason)
					.Select(() => premiumRaskatGAZelleAlias.TotalMoney).WithAlias(() => resultAlias.PremiumSum)
				).OrderBy(o => o.Date).Desc
				.TransformUsing(Transformers.AliasToBean<PremiumJournalNode<PremiumRaskatGAZelle>>());

			return resultQuery;
		}

		private void CreateCustomDeleteAction()
		{
			var deleteAction = new JournalAction("Удалить",
				(selected) =>
				{
					var selectedNodes = selected.OfType<PremiumJournalNode>();
					if(selectedNodes == null || selectedNodes.Count() != 1)
					{
						return false;
					}
					PremiumJournalNode selectedNode = selectedNodes.First();
					if(selectedNode.EntityType == typeof(PremiumRaskatGAZelle))
					{
						return false;
					}
					if(!EntityConfigs.ContainsKey(selectedNode.EntityType))
					{
						return false;
					}
					var config = EntityConfigs[selectedNode.EntityType];
					return config.PermissionResult.CanDelete;
				},
				(selected) => true,
				(selected) =>
				{
					var selectedNodes = selected.OfType<PremiumJournalNode>();
					if(selectedNodes == null || selectedNodes.Count() != 1)
					{
						return;
					}
					PremiumJournalNode selectedNode = selectedNodes.First();
					if(!EntityConfigs.ContainsKey(selectedNode.EntityType))
					{
						return;
					}
					var config = EntityConfigs[selectedNode.EntityType];
					if(config.PermissionResult.CanDelete)
					{
						DeleteHelper.DeleteEntity(selectedNode.EntityType, selectedNode.Id);
					}
				},
				"Delete"
			);
			NodeActionsList.Add(deleteAction);
		}
	}
}
