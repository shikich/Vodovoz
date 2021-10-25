using System;
using System.Linq;
using Autofac;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Logistic;
using Vodovoz.FilterViewModels.Employees;
using Vodovoz.Journals.JournalNodes;
using Vodovoz.ViewModels.Employees;

namespace Vodovoz.Journals.JournalViewModels.Employees
{
	public class FinesJournalViewModel : FilterableSingleEntityJournalViewModelBase<Fine, FineViewModel, FineJournalNode, FineFilterViewModel>
	{
		public FinesJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ILifetimeScope scope,
			INavigationManager navigationManager,
			params Action<FineFilterViewModel>[] filterParams)
			: base(unitOfWorkFactory,  commonServices, null, scope, navigationManager, false, false, filterParams)
		{
			TabName = "Журнал штрафов";
		}

		protected override Func<IUnitOfWork, IQueryOver<Fine>> ItemsSourceQueryFunction => uow => {
			FineJournalNode resultAlias = null;
			Fine fineAlias = null;
			FineItem fineItemAlias = null;
			Employee employeeAlias = null;
			RouteList routeListAlias = null;

			var query = uow.Session.QueryOver<Fine>(() => fineAlias)
				.JoinAlias(f => f.Items, () => fineItemAlias)
				.JoinAlias(() => fineItemAlias.Employee, () => employeeAlias)
				.JoinAlias(f => f.RouteList, () => routeListAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

			if(FilterViewModel.Subdivision != null) {
				query.Where(() => employeeAlias.Subdivision.Id == FilterViewModel.Subdivision.Id);
			}

			if(FilterViewModel.FineDateStart.HasValue) {
				query.Where(() => fineAlias.Date >= FilterViewModel.FineDateStart.Value);
			}

			if(FilterViewModel.FineDateEnd.HasValue) {
				query.Where(() => fineAlias.Date <= FilterViewModel.FineDateEnd.Value);
			}

			if(FilterViewModel.RouteListDateStart.HasValue) {
				query.Where(() => routeListAlias.Date >= FilterViewModel.RouteListDateStart.Value);
			}

			if(FilterViewModel.RouteListDateEnd.HasValue) {
				query.Where(() => routeListAlias.Date <= FilterViewModel.RouteListDateEnd.Value);
			}

			if (FilterViewModel.ExcludedIds != null && FilterViewModel.ExcludedIds.Any())
				query.WhereRestrictionOn(() => fineAlias.Id).Not.IsIn(FilterViewModel.ExcludedIds);
			
			if (FilterViewModel.FindFinesWithIds != null && FilterViewModel.FindFinesWithIds.Any())
				query.WhereRestrictionOn(() => fineAlias.Id).IsIn(FilterViewModel.FindFinesWithIds);

			query.Where(GetSearchCriterion(
				() => fineAlias.Id,
				() => fineAlias.TotalMoney,
				() => fineAlias.FineReasonString,
				() => employeeAlias.Name,
				() => employeeAlias.LastName,
				() => employeeAlias.Patronymic
			));

			return query
				.SelectList(list => list
					.SelectGroup(() => fineAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => fineAlias.Date).WithAlias(() => resultAlias.Date)
					.Select(Projections.SqlFunction(
						new SQLFunctionTemplate(NHibernateUtil.String, "GROUP_CONCAT( ?1 SEPARATOR ?2)"),
						NHibernateUtil.String,
						Projections.SqlFunction(new StandardSQLFunction("CONCAT_WS"),
							NHibernateUtil.String,
							Projections.Constant(" "),
							Projections.Property(() => employeeAlias.LastName),
							Projections.Property(() => employeeAlias.Name),
							Projections.Property(() => employeeAlias.Patronymic)
						),
						Projections.Constant("\n"))).WithAlias(() => resultAlias.EmployeesName)
					.Select(() => fineAlias.FineReasonString).WithAlias(() => resultAlias.FineReason)
					.Select(() => fineAlias.TotalMoney).WithAlias(() => resultAlias.FineSumm)
				).OrderBy(o => o.Date).Desc.OrderBy(o => o.Id).Desc
				.TransformUsing(Transformers.AliasToBean<FineJournalNode>());
		};

		protected override Func<FineViewModel> CreateDialogFunction => () =>
		{
			var scope = Scope.BeginLifetimeScope();
			return scope.Resolve<FineViewModel>(
				new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
		};

		protected override Func<FineJournalNode, FineViewModel> OpenDialogFunction => (node) =>
		{
			var scope = Scope.BeginLifetimeScope();
			return scope.Resolve<FineViewModel>(
				new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
		};
	}
}
