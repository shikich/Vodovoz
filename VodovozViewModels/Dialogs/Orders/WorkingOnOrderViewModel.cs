using System.Collections.Generic;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class WorkingOnOrderViewModel
    {
        public readonly IEnumerable<NonReturnReason> nonReturnReasons;

        public WorkingOnOrderViewModel(IUnitOfWork uow)
        {
            nonReturnReasons = uow.Session.QueryOver<NonReturnReason>().List();
        }
    }
}
