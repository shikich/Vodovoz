using System;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Journal;
using QS.Services;
using Vodovoz.ViewModels.Journals.JournalViewModels.Rent;

namespace Vodovoz.Factories
{
    public class RentPackagesJournalsViewModelsFactory : IRentPackagesJournalsViewModelsFactory
    {
        private readonly IUnitOfWorkFactory uowFactory;
        private readonly IInteractiveService interactiveService;
        private readonly INavigationManager navigationManager;

        public RentPackagesJournalsViewModelsFactory(
            IUnitOfWorkFactory uowFactory,
            IInteractiveService interactiveService,
            INavigationManager navigationManager)
        {
            this.uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            this.navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public PaidRentPackagesJournalViewModel CreatePaidRentPackagesJournalViewModel(
            bool multipleSelect = false, bool isCreateVisible = true, bool isEditVisible = true, bool isDeleteVisible = true)
        {
            var journal = new PaidRentPackagesJournalViewModel(uowFactory, interactiveService, navigationManager)
            {
                SelectionMode = multipleSelect == false
                    ? JournalSelectionMode.Single
                    : JournalSelectionMode.Multiple,
                VisibleCreateAction = isCreateVisible,
                VisibleEditAction = isEditVisible,
                VisibleDeleteAction = isDeleteVisible
            };

            return journal;
        }
        
        public FreeRentPackagesJournalViewModel CreateFreeRentPackagesJournalViewModel(
            bool multipleSelect = false, bool isCreateVisible = true, bool isEditVisible = true, bool isDeleteVisible = true)
        {
            var journal = new FreeRentPackagesJournalViewModel(uowFactory, interactiveService, navigationManager)
            {
                SelectionMode = multipleSelect == false
                    ? JournalSelectionMode.Single
                    : JournalSelectionMode.Multiple,
                VisibleCreateAction = isCreateVisible,
                VisibleEditAction = isEditVisible,
                VisibleDeleteAction = isDeleteVisible
            };

            return journal;
        }
    }
}