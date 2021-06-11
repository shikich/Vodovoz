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
    public class FreeRentPackagesJournalViewModel 
        : EntityJournalViewModelBase<FreeRentPackage, FreeRentPackageViewModel, FreeRentPackagesJournalNode>
    {
        private readonly ICommonServices _commonServices;
        
        public FreeRentPackagesJournalViewModel(
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
            
            NavigationManager.OpenViewModelTypedArgs<FreeRentPackageViewModel>(this, ctorTypes, ctorValues);
        }

        protected override void EditEntityDialog(FreeRentPackagesJournalNode node)
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
            
            NavigationManager.OpenViewModelTypedArgs<FreeRentPackageViewModel>(this, ctorTypes, ctorValues);
        }
    }
}
