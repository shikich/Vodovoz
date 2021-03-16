using Vodovoz.Domain.Goods;
using Vodovoz.FilterViewModels.Goods;

namespace Vodovoz.Factories
{
    public class NomenclaturesFilterViewModelFactory : INomenclatureFilterViewModelFactory
    {
        public NomenclatureFilterViewModel CreateNomenclatureFilterViewModel()
            => new NomenclatureFilterViewModel();

        public NomenclatureFilterViewModel CreateNomenclatureForSaleFilter(NomenclatureCategory category)
        {
            var filter = new NomenclatureFilterViewModel();
            
            filter.SetAndRefilterAtOnce(
                x => x.AvailableCategories = Nomenclature.GetCategoriesForSaleToOrder(),
                x => x.SelectCategory = category,
                x => x.SelectSaleCategory = SaleCategory.forSale,
                x => x.RestrictArchive = false
            );

            return filter;
        }
        
        public NomenclatureFilterViewModel CreateEquipmentFilter()
        {
            var filter = new NomenclatureFilterViewModel();
            
            filter.SetAndRefilterAtOnce(
                x => x.SelectCategory = NomenclatureCategory.equipment,
                x => x.RestrictArchive = false
            );

            return filter;
        }
        
        public NomenclatureFilterViewModel CreateMovementItemsFilter()
        {
            var filter = new NomenclatureFilterViewModel();
            
            filter.SetAndRefilterAtOnce(
                x => x.AvailableCategories = Nomenclature.GetCategoriesForGoods(),
                x => x.SelectCategory = NomenclatureCategory.equipment,
                x => x.SelectSaleCategory = SaleCategory.notForSale
            );

            return filter;
        }
    }
}