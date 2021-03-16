using Vodovoz.Domain.Goods;
using Vodovoz.FilterViewModels.Goods;

namespace Vodovoz.Factories
{
    public interface INomenclatureFilterViewModelFactory
    {
        NomenclatureFilterViewModel CreateNomenclatureFilterViewModel();
        NomenclatureFilterViewModel CreateNomenclatureForSaleFilter(NomenclatureCategory category);
        NomenclatureFilterViewModel CreateEquipmentFilter();
        NomenclatureFilterViewModel CreateMovementItemsFilter();
    }
}