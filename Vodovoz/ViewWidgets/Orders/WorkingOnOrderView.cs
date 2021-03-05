using QS.Views.GtkUI;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class WorkingOnOrderView : WidgetViewBase<WorkingOnOrderViewModel>
    {
        public WorkingOnOrderView(WorkingOnOrderViewModel viewModel) : base(viewModel)
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
