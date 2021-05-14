using QS.Views.GtkUI;
using Vodovoz.Domain.Orders;
using Vodovoz.Infrastructure.Converters;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class WorkingOnOrderView : WidgetViewBase<WorkingOnOrderViewModel>
    {
        public WorkingOnOrderView(WorkingOnOrderViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }
        
        //TODO Проверить свойства для комментариев ОДЗ и отдела продаж
        private void Configure()
        {
            enumDiverCallType.ItemsEnum = typeof(DriverCallType);
            
            specCmbNonReturnTareReasons.ItemsList = ViewModel.nonReturnReasons;
            
            ylblValueNumOfDriverCall.Binding.AddBinding(ViewModel.Order, o => o.DriverCallNumber, w => w.LabelProp, new IntToStringConverter()).InitializeFromSource();
            
            ytxtManagerComment.Binding.AddBinding(ViewModel.Order, o => o.CommentManager, w => w.Buffer.Text).InitializeFromSource();
            ytxtTareComment.Binding.AddBinding(ViewModel.Order, o => o.TareInformation, w => w.Buffer.Text).InitializeFromSource();
            ytxtSalesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.SalesDepartmentComment, w => w.Buffer.Text).InitializeFromSource();
            ytxtODZComment.Binding.AddBinding(ViewModel, vm => vm.ReceivablesDepartmentComment, w => w.Buffer.Text).InitializeFromSource();
        }
    }
}
