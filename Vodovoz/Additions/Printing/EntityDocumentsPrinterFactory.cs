using System.Collections.Generic;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.PrintableDocuments;
using Vodovoz.ViewModels.Infrastructure.Print;

namespace Vodovoz.Additions.Printing
{
    public class EntityDocumentsPrinterFactory : IEntityDocumentsPrinterFactory
    {
        public IEntityDocumentsPrinter CreateRouteListDocumentsPrinter(
            IUnitOfWork uow,
            RouteList routeList,
            IEntityDocumentsPrinterFactory entityDocumentsPrinterFactory,
            RouteListPrintableDocuments selectedType)
        {
            return new EntityDocumentsPrinter(uow, routeList, this, selectedType);
        }
        
        public IEntityDocumentsPrinter CreateOrderDocumentsPrinter(
            Order currentOrder, 
            bool? hideSignaturesAndStamps = null, 
            IList<OrderDocumentType> orderDocumentTypesToSelect = null)
        {
            return new EntityDocumentsPrinter(currentOrder, hideSignaturesAndStamps, orderDocumentTypesToSelect);
        }

        public IEntityDocumentsPrinter CreateOrderDocumentsPrinter(
            OrderBase currentOrder,
            IUnitOfWork uow,
            bool? hideSignaturesAndStamps = null,
            IList<OrderDocumentType> orderDocumentTypesToSelect = null)
        {
            return new EntityDocumentsPrinter(currentOrder, uow, hideSignaturesAndStamps, orderDocumentTypesToSelect);
        }

        public IEntityDocumentsPrinter CreateRouteListWithOrderDocumentsPrinter(
            IUnitOfWork uow,
            RouteList routeList,
            IEntityDocumentsPrinterFactory entityDocumentsPrinterFactory,
            RouteListPrintableDocuments[] routeListPrintableDocumentTypes,
            IList<OrderDocumentType> orderDocumentTypes = null)
        {
            return new EntityDocumentsPrinter(uow, routeList, this, routeListPrintableDocumentTypes, orderDocumentTypes);
        }
    }
}