using Vodovoz.FilterViewModels.Goods;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.Factories
{
    public interface INomenclaturesJournalViewModelFactory
    {
        NomenclaturesJournalViewModel CreateNomenclaturesJournalViewModel(
            NomenclatureFilterViewModel filterViewModel, bool multipleSelect = false);
    }
}