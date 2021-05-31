using System.Collections.Generic;
using System.Runtime.CompilerServices;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.PrintableDocuments;

namespace Vodovoz.ViewModels.Infrastructure.Print
{
    public interface IEntityDocumentsPrinterFactory
    {
        IEntityDocumentsPrinter CreateRouteListDocumentsPrinter(
            IUnitOfWork uow,
            RouteList routeList,
            IEntityDocumentsPrinterFactory entityDocumentsPrinterFactory,
            RouteListPrintableDocuments selectedType);

        IEntityDocumentsPrinter CreateOrderDocumentsPrinter(
            Order currentOrder,
            bool? hideSignaturesAndStamps = null,
            IList<OrderDocumentType> orderDocumentTypesToSelect = null);
        
        IEntityDocumentsPrinter CreateOrderDocumentsPrinter(
            OrderBase currentOrder,
            IUnitOfWork uow,
            bool? hideSignaturesAndStamps = null,
            IList<OrderDocumentType> orderDocumentTypesToSelect = null);

        IEntityDocumentsPrinter CreateRouteListWithOrderDocumentsPrinter(
            IUnitOfWork uow,
            RouteList routeList,
            IEntityDocumentsPrinterFactory entityDocumentsPrinterFactory,
            RouteListPrintableDocuments[] routeListPrintableDocumentTypes,
            IList<OrderDocumentType> orderDocumentTypes = null);
    }
}