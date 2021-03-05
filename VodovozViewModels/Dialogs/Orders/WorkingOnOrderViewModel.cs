using System.Collections.Generic;
using QS.DomainModel.UoW;
using QS.ViewModels;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class WorkingOnOrderViewModel : UoWWidgetViewModelBase
    {
        public OrderBase Order { get; set; }
        public readonly IEnumerable<NonReturnReason> nonReturnReasons;

        public WorkingOnOrderViewModel(OrderBase order)
        {
            Order = order;
            //UoW = uow ?? throw new ArgumentNullException(nameof(uow));
            //nonReturnReasons = uow.Session.QueryOver<NonReturnReason>().List();
        }
    }
}
