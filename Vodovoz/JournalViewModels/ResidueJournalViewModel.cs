using System;
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
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalNodes;
using Vodovoz.ViewModels;

namespace Vodovoz.JournalViewModels
{
	public class ResidueJournalViewModel : FilterableSingleEntityJournalViewModelBase<Residue, ResidueViewModel, ResidueJournalNode, ResidueFilterViewModel>
	{
		public ResidueJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ILifetimeScope scope,
			INavigationManager navigationManager,
			params Action<ResidueFilterViewModel>[] filterParams) 
			: base(unitOfWorkFactory, commonServices, null, scope, navigationManager, false, false, filterParams)
		{
			TabName = "Журнал остатков";

			SetOrder(x => x.Date, true);
			UpdateOnChanges(
				typeof(Residue)
			);
		}

		protected override Func<IUnitOfWork, IQueryOver<Residue>> ItemsSourceQueryFunction => (uow) => {
			Counterparty counterpartyAlias = null;
			Employee authorAlias = null;
			Employee lastEditorAlias = null;
			ResidueJournalNode resultAlias = null;
			Residue residueAlias = null;
			DeliveryPoint deliveryPointAlias = null;

			var residueQuery = uow.Session.QueryOver<Residue>(() => residueAlias)
				.JoinQueryOver(() => residueAlias.Customer, () => counterpartyAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
				.JoinQueryOver(() => residueAlias.DeliveryPoint, () => deliveryPointAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
				.JoinQueryOver(() => residueAlias.LastEditAuthor, () => lastEditorAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
				.JoinQueryOver(() => residueAlias.Author, () => authorAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

			if(FilterViewModel != null) {
				var dateCriterion = Projections.SqlFunction(
					   new SQLFunctionTemplate(
						   NHibernateUtil.Date,
						   "Date(?1)"
						  ),
					   NHibernateUtil.Date,
					   Projections.Property(() => residueAlias.Date)
					);

				if(FilterViewModel.StartDate.HasValue) {
					residueQuery.Where(Restrictions.Ge(dateCriterion, FilterViewModel.StartDate.Value));
				}

				if(FilterViewModel.EndDate.HasValue) {
					residueQuery.Where(Restrictions.Le(dateCriterion, FilterViewModel.EndDate.Value));
				}
			}

			residueQuery.Where(GetSearchCriterion(
				() => residueAlias.Id,
				() => counterpartyAlias.Name,
				() => deliveryPointAlias.CompiledAddress
			));

			var resultQuery = residueQuery
				.SelectList(list => list
				   .Select(() => residueAlias.Id).WithAlias(() => resultAlias.Id)
				   .Select(() => residueAlias.Date).WithAlias(() => resultAlias.Date)
				   .Select(() => counterpartyAlias.Name).WithAlias(() => resultAlias.Counterparty)
				   .Select(() => deliveryPointAlias.ShortAddress).WithAlias(() => resultAlias.DeliveryPoint)
				   .Select(() => authorAlias.LastName).WithAlias(() => resultAlias.AuthorSurname)
				   .Select(() => authorAlias.Name).WithAlias(() => resultAlias.AuthorName)
				   .Select(() => authorAlias.Patronymic).WithAlias(() => resultAlias.AuthorPatronymic)
				   .Select(() => lastEditorAlias.LastName).WithAlias(() => resultAlias.LastEditorSurname)
				   .Select(() => lastEditorAlias.Name).WithAlias(() => resultAlias.LastEditorName)
				   .Select(() => lastEditorAlias.Patronymic).WithAlias(() => resultAlias.LastEditorPatronymic)
				   .Select(() => residueAlias.LastEditTime).WithAlias(() => resultAlias.LastEditedTime)
				)
				.OrderBy(() => residueAlias.Date).Desc
				.TransformUsing(Transformers.AliasToBean<ResidueJournalNode>());
			return resultQuery;
		};

		protected override Func<ResidueViewModel> CreateDialogFunction => () =>
		{
			var scope = Scope.BeginLifetimeScope();
			return scope.Resolve<ResidueViewModel>(
				new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
		};

		protected override Func<ResidueJournalNode, ResidueViewModel> OpenDialogFunction => (node) =>
		{
			var scope = Scope.BeginLifetimeScope();
			return scope.Resolve<ResidueViewModel>(
				new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
		};
	}
}
