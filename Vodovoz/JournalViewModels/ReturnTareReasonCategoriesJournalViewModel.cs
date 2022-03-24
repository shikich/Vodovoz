using System;
using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Journal.DataLoader;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.JournalNodes;
using Vodovoz.ViewModels.Orders;

namespace Vodovoz.JournalViewModels
{
    public class ReturnTareReasonCategoriesJournalViewModel : SingleEntityJournalViewModelBase<ReturnTareReasonCategory, ReturnTareReasonCategoryViewModel, ReturnTareReasonCategoriesJournalNode>
    {
		public ReturnTareReasonCategoriesJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			INavigationManager navigationManager = null,
			bool hideJournalForOpenDialog = false,
			bool hideJournalForCreateDialog = false)
			: base(unitOfWorkFactory, commonServices, navigationManager, hideJournalForOpenDialog, hideJournalForCreateDialog)
		{
			TabName = "Категории причин забора тары";

			var threadLoader = DataLoader as ThreadDataLoader<ReturnTareReasonCategoriesJournalNode>;
			threadLoader.MergeInOrderBy(x => x.Id, false);

			UpdateOnChanges(typeof(ReturnTareReasonCategory));
		}

		protected override Func<IUnitOfWork, IQueryOver<ReturnTareReasonCategory>> ItemsSourceQueryFunction => (uow) => {
			ReturnTareReasonCategoriesJournalNode resultAlias = null;

			var query = uow.Session.QueryOver<ReturnTareReasonCategory>();
			
			query.Where(
				GetSearchCriterion<ReturnTareReasonCategory>(
					x => x.Id
				)
			);

			var result = query.SelectList(list => list
									.Select(x => x.Id).WithAlias(() => resultAlias.Id)
									.Select(x => x.Name).WithAlias(() => resultAlias.Name))
									.TransformUsing(Transformers.AliasToBean<ReturnTareReasonCategoriesJournalNode>())
									.OrderBy(x => x.Name).Asc;
			return result;
		};

		protected override Func<ReturnTareReasonCategoryViewModel> CreateDialogFunction => () => new ReturnTareReasonCategoryViewModel(
			EntityUoWBuilder.ForCreate(),
			UnitOfWorkFactory,
			commonServices
		);

		protected override Func<ReturnTareReasonCategoriesJournalNode, ReturnTareReasonCategoryViewModel> OpenDialogFunction => node => new ReturnTareReasonCategoryViewModel(
			EntityUoWBuilder.ForOpen(node.Id),
			UnitOfWorkFactory,
			commonServices
	   	);

		protected override void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultSelectAction();
			CreateDefaultAddActions();
			CreateDefaultEditAction();
		}
    }
}
