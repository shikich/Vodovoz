using System;
using Autofac;
using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.ViewModels.Journals.JournalNodes;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Orders
{
	public class DiscountReasonJournalViewModel
		: SingleEntityJournalViewModelBase<DiscountReason, DiscountReasonViewModel, DiscountReasonJournalNode>
	{
		public DiscountReasonJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ILifetimeScope scope = null,
			INavigationManager navigationManager = null,
			bool hideJournalForOpen = false,
			bool hideJournalForCreate = false)
			: base(unitOfWorkFactory, commonServices, scope, navigationManager, hideJournalForOpen, hideJournalForCreate)
		{
			TabName = "Журнал оснований для скидки";

			UpdateOnChanges(typeof(DiscountReason));
		}

		protected override void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultSelectAction();
			CreateDefaultEditAction();
			CreateDefaultAddActions();
		}

		protected override Func<IUnitOfWork, IQueryOver<DiscountReason>> ItemsSourceQueryFunction => (uow) =>
		{
			DiscountReason drAlias = null;
			DiscountReasonJournalNode drNodeAlias = null;

			var query = uow.Session.QueryOver(() => drAlias);

			query.Where(GetSearchCriterion(
				() => drAlias.Id,
				() => drAlias.Name
			));
			var result = query.SelectList(list => list
					.Select(dr => dr.Id).WithAlias(() => drNodeAlias.Id)
					.Select(dr => dr.Name).WithAlias(() => drNodeAlias.Name)
					.Select(dr => dr.IsArchive).WithAlias(() => drNodeAlias.IsArchive))
				.OrderBy(dr => dr.IsArchive).Asc
				.OrderBy(dr => dr.Name).Asc
				.TransformUsing(Transformers.AliasToBean<DiscountReasonJournalNode>());
			return result;
		};

		protected override Func<DiscountReasonViewModel> CreateDialogFunction =>
			() => new DiscountReasonViewModel(
				EntityUoWBuilder.ForCreate(),
				QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
				commonServices);

		protected override Func<DiscountReasonJournalNode, DiscountReasonViewModel> OpenDialogFunction =>
			(node) => new DiscountReasonViewModel(
				EntityUoWBuilder.ForOpen(node.Id),
				QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
				commonServices);
	}
}
