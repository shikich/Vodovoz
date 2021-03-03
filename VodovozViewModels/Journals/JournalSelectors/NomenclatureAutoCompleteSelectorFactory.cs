using System;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.Infrastructure.Services;

namespace Vodovoz.ViewModels.Journals.JournalSelectors
{
	public class NomenclatureAutoCompleteSelectorFactory<Nomenclature, NomenclaturesJournalViewModel> :
			NomenclatureSelectorFactory<Nomenclature, NomenclaturesJournalViewModel>, IEntityAutocompleteSelectorFactory
		where NomenclaturesJournalViewModel : JournalViewModelBase, IEntityAutocompleteSelector
	{
		public NomenclatureAutoCompleteSelectorFactory(
			ICommonServices commonServices,
			IEmployeeService employeeService,
			NomenclatureFilterViewModel filterViewModel,
			IEntityAutocompleteSelectorFactory counterpartySelectorFactory,
			INomenclatureRepository nomenclatureRepository,
			IUserRepository userRepository) 
			: base(commonServices, employeeService, filterViewModel, counterpartySelectorFactory, nomenclatureRepository,
				userRepository) { }

		public IEntityAutocompleteSelector CreateAutocompleteSelector(bool multipleSelect = false)
		{
			NomenclaturesJournalViewModel selectorViewModel = (NomenclaturesJournalViewModel)Activator
			.CreateInstance(typeof(NomenclaturesJournalViewModel), new object[] { filter, 
				UnitOfWorkFactory.GetDefaultFactory, commonServices, employeeService, 
				this, counterpartySelectorFactory, nomenclatureRepository, userRepository});
			
			selectorViewModel.SelectionMode = JournalSelectionMode.Single;
			return selectorViewModel;
		}
	}
}
