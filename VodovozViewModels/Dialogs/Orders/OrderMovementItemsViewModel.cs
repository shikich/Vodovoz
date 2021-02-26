using System;
using QS.ViewModels;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderMovementItemsViewModel : UoWWidgetViewModelBase
    {
        public OrderBase Order { get; set; }

        public OrderMovementItemsViewModel()
        {
        }
    }
}
