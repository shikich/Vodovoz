using System.Collections.Generic;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Orders.Documents.AssemblyList;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Domain.Orders.Documents.Certificate;
using Vodovoz.Domain.Orders.Documents.DoneWork;
using Vodovoz.Domain.Orders.Documents.DriverTicket;
using Vodovoz.Domain.Orders.Documents.Equipment;
using Vodovoz.Domain.Orders.Documents.Invoice;
using Vodovoz.Domain.Orders.Documents.OrderContract;
using Vodovoz.Domain.Orders.Documents.OrderM2Proxy;
using Vodovoz.Domain.Orders.Documents.ShetFactura;
using Vodovoz.Domain.Orders.Documents.Torg12;
using Vodovoz.Domain.Orders.Documents.Torg2;
using Vodovoz.Domain.Orders.Documents.TransportInvoice;
using Vodovoz.Domain.Orders.Documents.UPD;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.Parameters;

namespace Vodovoz.Domain.Orders.Documents {
    public class OrderDocumentUpdatersFactory : IOrderDocumentUpdatersFactory
    {
        public IEnumerable<OrderDocumentUpdaterBase> CreateUpdaters() {
            yield return CreateAssemblyListDocumentUpdater();
            yield return CreateBillDocumentUpdater();
            yield return CreateSpecialBillDocumentUpdater();
            yield return CreateNomenclatureCertificateDocumentUpdater();
            yield return CreateDoneWorkDocumentUpdater();
            yield return CreateDriverTicketDocumentUpdater();
            yield return CreateEquipmentReturnDocumentUpdater();
            yield return CreateEquipmentTransferDocumentUpdater();
            yield return CreateInvoiceBarterDocumentUpdater();
            yield return CreateInvoiceContractDocumentUpdater();
            yield return CreateInvoiceDocumentUpdater();
            yield return CreateOrderContractDocumentUpdater();
            yield return CreateOrderM2ProxyDocumentUpdater();
            yield return CreateShetFacturaDocumentUpdater();
            yield return CreateTorg12DocumentUpdater();
            yield return CreateTorg2DocumentUpdater();
            yield return CreateTransportInvoiceDocumentUpdater();
            yield return CreateUPDDocumentUpdater();
            yield return CreateSpecialUPDDocumentUpdater();
        }

        private OrderDocumentUpdaterBase CreateAssemblyListDocumentUpdater() {
            var assemblyListDocumentFactory = new AssemblyListDocumentFactory();
            
            return new AssemblyListDocumentUpdater(assemblyListDocumentFactory);
        }
        
        private OrderDocumentUpdaterBase CreateBillDocumentUpdater() {
            var billDocumentFactory = new BillDocumentFactory();
            var nomemnclatureParametersProvider = new NomenclatureParametersProvider();
            
            return new BillDocumentUpdater(billDocumentFactory, nomemnclatureParametersProvider);
        }

        private OrderDocumentUpdaterBase CreateSpecialBillDocumentUpdater() {
            var specialBillDocumentFactory = new SpecialBillDocumentFactory();
            
            return new SpecialBillDocumentUpdater(specialBillDocumentFactory, CreateBillDocumentUpdater() as BillDocumentUpdater);
        }
        
        private OrderDocumentUpdaterBase CreateNomenclatureCertificateDocumentUpdater() {
            var nomenclatureCertificateDocumentFactory = new NomenclatureCertificateDocumentFactory();
            var nomenclatureRepository = new NomenclatureRepository(new NomenclatureParametersProvider());
            var uow = UnitOfWorkFactory.CreateWithoutRoot();
            
            return new NomenclatureCertificateDocumentUpdater(nomenclatureCertificateDocumentFactory, nomenclatureRepository, uow);
        }
        
        private OrderDocumentUpdaterBase CreateDoneWorkDocumentUpdater() {
            var doneWorkDocumentFactory = new DoneWorkDocumentFactory();
            
            return new DoneWorkDocumentUpdater(doneWorkDocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateDriverTicketDocumentUpdater() {
            var driverTicketDocumentFactory = new DriverTicketDocumentFactory();
            
            return new DriverTicketDocumentUpdater(driverTicketDocumentFactory, CreateBillDocumentUpdater() as BillDocumentUpdater);
        }

        private OrderDocumentUpdaterBase CreateEquipmentReturnDocumentUpdater() {
            var equipmentReturnDocumentFactory = new EquipmentReturnDocumentFactory();
            
            return new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateEquipmentTransferDocumentUpdater() {
            var equipmentTransferDocumentFactory = new EquipmentTransferDocumentFactory();
            
            return new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateInvoiceBarterDocumentUpdater() {
            var invoiceBarterDocumentFactory = new InvoiceBarterDocumentFactory();
            
            return new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateInvoiceContractDocumentUpdater() {
            var invoiceContractDocumentFactory = new InvoiceContractDocumentFactory();
            
            return new InvoiceContractDocumentUpdater(invoiceContractDocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateInvoiceDocumentUpdater() {
            var invoiceDocumentFactory = new InvoiceDocumentFactory();
            var nomemnclatureParametersProvider = new NomenclatureParametersProvider();

            return new InvoiceDocumentUpdater(invoiceDocumentFactory, nomemnclatureParametersProvider);
        }

        private OrderDocumentUpdaterBase CreateOrderContractDocumentUpdater() {
            var orderContractDocumentFactory = new OrderContractDocumentFactory();
            
            return new OrderContractDocumentUpdater(orderContractDocumentFactory);
        }
        
        private OrderDocumentUpdaterBase CreateOrderM2ProxyDocumentUpdater() {
            var orderM2ProxyDocumentFactory = new OrderM2ProxyDocumentFactory();
            
            return new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateShetFacturaDocumentUpdater() {
            var shetFacturaDocumentFactory = new ShetFacturaDocumentFactory();
            
            return new ShetFacturaDocumentUpdater(shetFacturaDocumentFactory, CreateUPDDocumentUpdater() as UPDDocumentUpdater);
        }

        private OrderDocumentUpdaterBase CreateTorg12DocumentUpdater() {
            var torg12DocumentFactory = new Torg12DocumentFactory();
            
            return new Torg12DocumentUpdater(torg12DocumentFactory, CreateUPDDocumentUpdater() as UPDDocumentUpdater);
        }

        private OrderDocumentUpdaterBase CreateTorg2DocumentUpdater() {
            var torg2DocumentFactory = new Torg2DocumentFactory();
            
            return new Torg2DocumentUpdater(torg2DocumentFactory);
        }

        private OrderDocumentUpdaterBase CreateTransportInvoiceDocumentUpdater() {
            var transportInvoiceDocumentFactory = new TransportInvoiceDocumentFactory();
            var nomemnclatureParametersProvider = new NomenclatureParametersProvider();

            return new TransportInvoiceDocumentUpdater(transportInvoiceDocumentFactory, nomemnclatureParametersProvider);
        }

        private OrderDocumentUpdaterBase CreateUPDDocumentUpdater() {
            var updDocumentFactory = new UPDDocumentFactory();
            
            return new UPDDocumentUpdater(updDocumentFactory, CreateBillDocumentUpdater() as BillDocumentUpdater);
        }

        private OrderDocumentUpdaterBase CreateSpecialUPDDocumentUpdater() {
            var specialUPDDocumentFactory = new SpecialUPDDocumentFactory();
            
            return new SpecialUPDDocumentUpdater(specialUPDDocumentFactory, CreateUPDDocumentUpdater() as UPDDocumentUpdater);
        }
    }
}