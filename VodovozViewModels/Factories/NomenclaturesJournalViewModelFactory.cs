using System;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.Infrastructure.Services;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.Factories
{
    public class NomenclaturesJournalViewModelFactory : INomenclaturesJournalViewModelFactory
    {
        private readonly IUnitOfWorkFactory uowFactory;
        private readonly ICommonServices commonServices;
        private readonly IEmployeeService employeeService;
        private readonly IEntityAutocompleteSelectorFactory nomenclatureSelectorFactory;
        private readonly IEntityAutocompleteSelectorFactory counterpartySelectorFactory;
        private readonly INomenclatureRepository nomenclatureRepository;
        private readonly IUserRepository userRepository;
        
        public NomenclaturesJournalViewModelFactory(
            IUnitOfWorkFactory uowFactory,
            ICommonServices commonServices,
            IEmployeeService employeeService,
            IEntityAutocompleteSelectorFactory nomenclatureSelectorFactory,
            IEntityAutocompleteSelectorFactory counterpartySelectorFactory,
            INomenclatureRepository nomenclatureRepository,
            IUserRepository userRepository)
        {
            this.uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
            this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
            this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            this.nomenclatureSelectorFactory = 
                nomenclatureSelectorFactory ?? throw new ArgumentNullException(nameof(nomenclatureSelectorFactory));
            this.counterpartySelectorFactory = 
                counterpartySelectorFactory ?? throw new ArgumentNullException(nameof(counterpartySelectorFactory));
            this.nomenclatureRepository = 
                nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        
        public NomenclaturesJournalViewModel CreateNomenclaturesJournalViewModel(
            NomenclatureFilterViewModel filterViewModel, bool multipleSelect = false)
                => new NomenclaturesJournalViewModel(
                    filterViewModel,
                    uowFactory,
                    commonServices,
                    employeeService,
                    nomenclatureSelectorFactory,
                    counterpartySelectorFactory,
                    nomenclatureRepository,
                    userRepository)
                {
                    SelectionMode = multipleSelect == false 
                        ? JournalSelectionMode.Single
                        : JournalSelectionMode.Multiple
                };
    }
}