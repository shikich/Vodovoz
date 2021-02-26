using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class WorkingOnOrderView : Gtk.Bin
    {
        private WorkingOnOrderViewModel ViewModel;

        public WorkingOnOrderView()
        {
            this.Build();
            //Configure();
        }

        private void Configure()
        {
            enumDiverCallType.ItemsEnum = typeof(DriverCallType);
            specCmbNonReturnTareReasons.ItemsList = ViewModel.nonReturnReasons;
        }
    }
}
