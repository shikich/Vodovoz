using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Journal;
using QS.Services;
using Vodovoz.Domain;
using Vodovoz.Journals.Nodes.Rent;
using Vodovoz.ViewModels.ViewModels.Rent;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Rent
{
    public class FreeRentPackagesJournalViewModel 
        : EntityJournalViewModelBase<FreeRentPackage, FreeRentPackageViewModel, FreeRentPackagesJournalNode>
    {
        public FreeRentPackagesJournalViewModel(
            IUnitOfWorkFactory unitOfWorkFactory,
            IInteractiveService interactiveService,
            INavigationManager navigationManager) : base(unitOfWorkFactory, interactiveService, navigationManager) { }

        protected override IQueryOver<FreeRentPackage> ItemsQuery(IUnitOfWork uow)
        {
            FreeRentPackagesJournalNode resultAlias = null;
            EquipmentType equipmentTypeAlias = null;

            return uow.Session.QueryOver<FreeRentPackage>()
                .Left.JoinAlias(x => x.EquipmentType, () => equipmentTypeAlias)
                .Where(GetSearchCriterion<FreeRentPackage>(x => x.Name))
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => resultAlias.Id)
                    .Select(x => x.Name).WithAlias(() => resultAlias.Name)
                    .Select(() => equipmentTypeAlias.Name).WithAlias(() => resultAlias.EquipmentTypeName))
                .OrderBy(x => x.Name).Asc
                .TransformUsing(Transformers.AliasToBean<FreeRentPackagesJournalNode>());
        }
    }
}