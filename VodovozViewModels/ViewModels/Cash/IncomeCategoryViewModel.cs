using System;
using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Cash;
using Vodovoz.ViewModels.Journals.JournalFactories;
using Vodovoz.ViewModels.Journals.JournalViewModels.Cash;

namespace Vodovoz.ViewModels.ViewModels.Cash
{
    public class IncomeCategoryViewModel: EntityTabViewModelBase<IncomeCategory>
    {
        public IncomeCategoryViewModel(
            IEntityUoWBuilder uowBuilder,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommonServices commonServices,
            ISubdivisionJournalFactory subdivisionJournalFactory,
			ILifetimeScope scope,
			INavigationManager navigationManager = null
        ) : base(uowBuilder, unitOfWorkFactory, commonServices, navigationManager, scope)
        {
	        if(subdivisionJournalFactory == null)
	        {
		        throw new ArgumentNullException(nameof(subdivisionJournalFactory));
	        }

			IncomeCategoryAutocompleteSelectorFactory =
				new EntityAutocompleteSelectorFactory<IncomeCategoryJournalViewModel>(
					typeof(IncomeCategory),
					() => Scope.Resolve<IncomeCategoryJournalViewModel>());

			SubdivisionAutocompleteSelectorFactory = subdivisionJournalFactory.CreateDefaultSubdivisionAutocompleteSelectorFactory(Scope);
            
            if(uowBuilder.IsNewEntity)
                TabName = "Создание новой категории дохода";
            else
                TabName = $"{Entity.Title}";
        }
        
        public IEntityAutocompleteSelectorFactory SubdivisionAutocompleteSelectorFactory { get; }
        
        public bool IsArchive
        {
            get { return Entity.IsArchive; }
            set
            {
                Entity.SetIsArchiveRecursively(value);
            }
        }
        
        public IEntityAutocompleteSelectorFactory IncomeCategoryAutocompleteSelectorFactory;
        
        #region Permissions
        public bool CanCreate => PermissionResult.CanCreate;
        public bool CanRead => PermissionResult.CanRead;
        public bool CanUpdate => PermissionResult.CanUpdate;
        public bool CanDelete => PermissionResult.CanDelete;

        public bool CanCreateOrUpdate => Entity.Id == 0 ? CanCreate : CanUpdate;

        #endregion
    }
}
