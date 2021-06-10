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
            if(ViewModel.Order is OrderFrom1c)
            {
                this.Sensitive = false;
            }
            
            enumDiverCallType.ItemsEnum = typeof(DriverCallType);
            enumDiverCallType.Binding.AddBinding(ViewModel.Order, o => o.DriverCallType, w => w.SelectedItem).InitializeFromSource();
            enumDiverCallType.Binding.AddBinding(ViewModel, vm => vm.CanEditOrder, w => w.Sensitive).InitializeFromSource();
            enumDiverCallType.Changed += ViewModel.OnEnumDiverCallTypeChanged;
                
            specCmbNonReturnTareReasons.ItemsList = ViewModel.nonReturnReasons;
            specCmbNonReturnTareReasons.Binding.AddBinding(ViewModel.Order, o => o.TareNonReturnReason, w => w.SelectedItem).InitializeFromSource();
            specCmbNonReturnTareReasons.Binding.AddBinding(ViewModel, vm => vm.CanEditOrder, w => w.Sensitive).InitializeFromSource();
            specCmbNonReturnTareReasons.ItemSelected += (sender, e) => ViewModel.Order.IsTareNonReturnReasonChangedByUser = true;
            
            ylblValueNumOfDriverCall.Binding.AddBinding(ViewModel.Order, o => o.DriverCallNumber, w => w.LabelProp, new IntToStringConverter()).InitializeFromSource();
            ylblValueNumOfDriverCall.Binding.AddBinding(ViewModel, vm => vm.CanEditOrder, w => w.Sensitive).InitializeFromSource();
            ylblSalesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.CanShowComments, w => w.Visible).InitializeFromSource();
            ylblReceivablesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.CanShowComments, w => w.Visible).InitializeFromSource();

            ytxtManagerComment.Binding.AddBinding(ViewModel.Order, o => o.CommentManager, w => w.Buffer.Text).InitializeFromSource();
            ytxtManagerComment.Binding.AddBinding(ViewModel, vm => vm.CanEditOrder, w => w.Sensitive).InitializeFromSource();
            ytxtTareComment.Binding.AddBinding(ViewModel.Order, o => o.TareInformation, w => w.Buffer.Text).InitializeFromSource();
            ytxtTareComment.Binding.AddBinding(ViewModel, vm => vm.CanEditOrder, w => w.Sensitive).InitializeFromSource();
            ytxtSalesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.SalesDepartmentComment, w => w.Buffer.Text).InitializeFromSource();
            ytxtSalesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.CanEditSalesDepartmentComment, w => w.Sensitive).InitializeFromSource();
            yhboxSalesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.CanShowComments, w => w.Visible).InitializeFromSource();
            ytxtReceivablesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.ReceivablesDepartmentComment, w => w.Buffer.Text).InitializeFromSource();
            ytxtReceivablesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.CanEditReceivablesDepartmentComment, w => w.Sensitive).InitializeFromSource();
            yhboxReceivablesDepartmentComment.Binding.AddBinding(ViewModel, vm => vm.CanShowComments, w => w.Visible).InitializeFromSource();
        }

        public override void Destroy()
        {
            enumDiverCallType.Changed -= ViewModel.OnEnumDiverCallTypeChanged;
            base.Destroy();
        }
    }
}
