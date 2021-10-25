using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using System;
using Autofac;
using QS.Navigation;
using QS.Project.DB;
using Vodovoz.Domain.Complaints;
using Vodovoz.ViewModels.Complaints;
using Vodovoz.ViewModels.Journals.FilterViewModels.Complaints;
using Vodovoz.ViewModels.Journals.JournalNodes.Complaints;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Complaints
{
	public class ComplaintKindJournalViewModel : FilterableSingleEntityJournalViewModelBase<ComplaintKind, ComplaintKindViewModel, ComplaintKindJournalNode, ComplaintKindJournalFilterViewModel>
	{
		public ComplaintKindJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ILifetimeScope scope,
			INavigationManager navigationManager = null,
			params Action<ComplaintKindJournalFilterViewModel>[] filterParams)
			: base(unitOfWorkFactory, commonServices, null, scope, navigationManager, false, false, filterParams)
		{
			TabName = "Виды рекламаций";

			UpdateOnChanges(
				typeof(ComplaintKind),
				typeof(ComplaintObject)
				);
		}

		protected override Func<IUnitOfWork, IQueryOver<ComplaintKind>> ItemsSourceQueryFunction => (uow) =>
		{
			ComplaintKind complaintKindAlias = null;
			ComplaintObject complaintObjectAlias = null;
			ComplaintKindJournalNode resultAlias = null;
			Subdivision subdivisionsAlias = null;

			var itemsQuery = uow.Session.QueryOver(() => complaintKindAlias)
				.Left.JoinAlias(x => x.ComplaintObject, () => complaintObjectAlias)
				.Left.JoinAlias(x => x.Subdivisions, () => subdivisionsAlias);

			if(FilterViewModel.ComplaintObject != null)
			{
				itemsQuery.Where(x => x.ComplaintObject.Id == FilterViewModel.ComplaintObject.Id);
			}

			itemsQuery.Where(GetSearchCriterion(
				() => complaintKindAlias.Id,
				() => complaintKindAlias.Name,
				() => complaintObjectAlias.Name)
			);

			var subdivisionsProjection = CustomProjections.GroupConcat(
				() => subdivisionsAlias.ShortName,
				orderByExpression: () => subdivisionsAlias.ShortName,
				separator: ", "
			);

			itemsQuery.SelectList(list => list
					.SelectGroup(() => complaintKindAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => complaintKindAlias.Name).WithAlias(() => resultAlias.Name)
					.Select(() => complaintKindAlias.IsArchive).WithAlias(() => resultAlias.IsArchive)
					.Select(() => complaintObjectAlias.Name).WithAlias(() => resultAlias.ComplaintObject)
					.Select(subdivisionsProjection).WithAlias(() => resultAlias.Subdivisions)
				)
				.TransformUsing(Transformers.AliasToBean<ComplaintKindJournalNode>());

			return itemsQuery;
		};

		protected override void CreateNodeActions()
		{
			CreateDefaultAddActions();
			CreateDefaultEditAction();
			CreateDefaultSelectAction();
		}

		protected override Func<ComplaintKindViewModel> CreateDialogFunction => () =>
		{
			var scope = Scope.BeginLifetimeScope();
			return scope.Resolve<ComplaintKindViewModel>(new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
		};

		protected override Func<ComplaintKindJournalNode, ComplaintKindViewModel> OpenDialogFunction =>
			node =>
			{
				var scope = Scope.BeginLifetimeScope();
				return scope.Resolve<ComplaintKindViewModel>(
					new TypedParameter(typeof(IEntityUoWBuilder),
					EntityUoWBuilder.ForOpen(node.Id)));
			};
	}
}
