using QS.DomainModel.UoW;
using Vodovoz.Domain.Goods;
using Vodovoz.Services;

namespace Vodovoz.Parameters
{
    public class NomenclatureParametersProvider : INomenclatureParametersProvider
    {
        private readonly ParametersProvider parametersProvider;
        
        public NomenclatureParametersProvider()
        {
            parametersProvider = ParametersProvider.Instance;
        }
        
        #region INomenclatureParametersProvider implementation

        public int Folder1cForOnlineStoreNomenclatures => parametersProvider.GetIntValue("folder_1c_for_online_store_nomenclatures");
        public int MeasurementUnitForOnlineStoreNomenclatures => parametersProvider.GetIntValue("measurement_unit_for_online_store_nomenclatures");
        public int RootProductGroupForOnlineStoreNomenclatures => parametersProvider.GetIntValue("root_product_group_for_online_store_nomenclatures");
        public int CurrentOnlineStoreId => parametersProvider.GetIntValue("current_online_store_id");
        public string OnlineStoreExportFileUrl => parametersProvider.GetStringValue("online_store_export_file_url");
        public int PaidDeliveryNomenclatureId => parametersProvider.GetIntValue("paid_delivery_nomenclature_id");
        public int WaterSemiozerieId => parametersProvider.GetIntValue("nomenclature_semiozerie_id");
        public int WaterKislorodnayaId => parametersProvider.GetIntValue("nomenclature_kislorodnaya_id");
        public int WaterSnyatogorskayaId => parametersProvider.GetIntValue("nomenclature_snyatogorskaya_id");
        public int WaterKislorodnayaDeluxeId => parametersProvider.GetIntValue("nomenclature_kislorodnaya_deluxe_id");
        public int WaterStroikaId => parametersProvider.GetIntValue("nomenclature_stroika_id");
        public int WaterRuchkiId => parametersProvider.GetIntValue("nomenclature_ruchki_id");
        
        #region Получение номенклатур воды

		public Nomenclature GetWaterSemiozerie(IUnitOfWork uow) => uow.GetById<Nomenclature>(WaterSemiozerieId);
		public Nomenclature GetWaterKislorodnaya(IUnitOfWork uow) => uow.GetById<Nomenclature>(WaterKislorodnayaId);
		public Nomenclature GetWaterSnyatogorskaya(IUnitOfWork uow) => uow.GetById<Nomenclature>(WaterSnyatogorskayaId);
		public Nomenclature GetWaterKislorodnayaDeluxe(IUnitOfWork uow) => uow.GetById<Nomenclature>(WaterKislorodnayaDeluxeId);
		public Nomenclature GetWaterStroika(IUnitOfWork uow) => uow.GetById<Nomenclature>(WaterStroikaId);
		public Nomenclature GetWaterRuchki(IUnitOfWork uow) => uow.GetById<Nomenclature>(WaterRuchkiId);
		public decimal GetWaterPriceIncrement => parametersProvider.GetDecimalValue("water_price_increment");

		#endregion

		#endregion INomenclatureParametersProvider implementation
    }
}