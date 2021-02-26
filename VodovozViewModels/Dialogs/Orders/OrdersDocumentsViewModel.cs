using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrdersDocumentsViewModel : OrdersDocumentsViewModelBase
    {
        public OrdersDocumentsViewModel(
            IUnitOfWork uow, 
            Counterparty counterparty
        ) : base(uow, counterparty)
        {
            
        }
    }
}
