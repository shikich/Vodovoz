using System.Collections.Generic;

namespace Vodovoz.Domain.Orders.Documents
{
    public interface IOrderDocumentUpdatersFactory
    {
        IEnumerable<OrderDocumentUpdaterBase> CreateUpdaters();
    }
}