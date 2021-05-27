using System;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Services;
using Vodovoz.Domain;
using Vodovoz.ViewModels.Journals.JournalViewModels.Rent;

namespace Vodovoz.Factories
{
    public class NonSerialEquipmentsForRentJournalViewModelFactory : INonSerialEquipmentsForRentJournalViewModelFactory
    {
        private readonly IUnitOfWorkFactory uowFactory;
        private readonly IInteractiveService interactiveService;
        private readonly INavigationManager navigationManager;

        public NonSerialEquipmentsForRentJournalViewModelFactory(
            IUnitOfWorkFactory uowFactory,
            IInteractiveService interactiveService,
            INavigationManager navigationManager)
        {
            this.uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            this.navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public NonSerialEquipmentsForRentJournalViewModel CreateNonSerialEquipmentsForRentJournalViewModel(EquipmentType equipmentType)
        {
            return new NonSerialEquipmentsForRentJournalViewModel(equipmentType, uowFactory, interactiveService, navigationManager);
        }
    }
}