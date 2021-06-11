using System;
using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using QS.Validation;
using Vodovoz.Domain;
using Vodovoz.Journals.Nodes.Rent;
using Vodovoz.ViewModels.ViewModels.Rent;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Rent
{
    public class PaidRentPackagesJournalViewModel
        : EntityJournalViewModelBase<PaidRentPackage, PaidRentPackageViewModel, PaidRentPackagesJournalNode>
    {
        private readonly ICommonServices _commonServices;
        
        public PaidRentPackagesJournalViewModel(
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommonServices commonServices,
            INavigationManager navigationManager) 
            : base(
                unitOfWorkFactory, 
                commonServices?.InteractiveService, 
                navigationManager,
                null, 
                commonServices?.CurrentPermissionService)
        {
            _commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
        }

        protected override IQueryOver<PaidRentPackage> ItemsQuery(IUnitOfWork uow)
        {
            PaidRentPackagesJournalNode resultAlias = null;
            EquipmentType equipmentTypeAlias = null;

            return uow.Session.QueryOver<PaidRentPackage>()
                .Left.JoinAlias(x => x.EquipmentType, () => equipmentTypeAlias)
                .Where(GetSearchCriterion<PaidRentPackage>(
                    x => x.Name, x => x.PriceDaily, x => x.PriceMonthly))
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => resultAlias.Id)
                    .Select(x => x.Name).WithAlias(() => resultAlias.Name)
                    .Select(x => x.PriceDaily).WithAlias(() => resultAlias.PriceDaily)
                    .Select(x => x.PriceMonthly).WithAlias(() => resultAlias.PriceMonthly)
                    .Select(() => equipmentTypeAlias.Name).WithAlias(() => resultAlias.EquipmentTypeName))
                .OrderBy(x => x.Name).Asc
                .TransformUsing(Transformers.AliasToBean<PaidRentPackagesJournalNode>());
        }
        
        protected override void CreateEntityDialog()
        {
            Type[] ctorTypes = 
            {
                typeof(IEntityUoWBuilder),
                typeof(IUnitOfWorkFactory),
                typeof(INavigationManager),
                typeof(IValidator),
            };

            object[] ctorValues = 
            {
                EntityUoWBuilder.ForCreate(),
                UnitOfWorkFactory,
                NavigationManager,
                _commonServices.ValidationService
            };
            
            NavigationManager.OpenViewModelTypedArgs<PaidRentPackageViewModel>(this, ctorTypes, ctorValues);
        }

        protected override void EditEntityDialog(PaidRentPackagesJournalNode node)
        {
            Type[] ctorTypes = 
            {
                typeof(IEntityUoWBuilder),
                typeof(IUnitOfWorkFactory),
                typeof(INavigationManager),
                typeof(IValidator),
            };

            object[] ctorValues = 
            {
                EntityUoWBuilder.ForOpen(node.Id),
                UnitOfWorkFactory,
                NavigationManager,
                _commonServices.ValidationService
            };
            
            NavigationManager.OpenViewModelTypedArgs<PaidRentPackageViewModel>(this, ctorTypes, ctorValues);
        }
    }
}
