using Vodovoz.Domain.Client;

namespace Vodovoz.Domain.Orders {
    public interface IDefaultOrderDocumentType {
        DefaultDocumentType? DefaultDocumentType { get; set; }
    }
}