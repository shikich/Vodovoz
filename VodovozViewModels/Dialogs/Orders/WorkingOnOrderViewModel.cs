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
        
        private string receivablesDepartmentComment;
        /// <summary>
        /// Комментарий отдела дебиторской задолженности
        /// </summary>
        public virtual string ReceivablesDepartmentComment {
            get => receivablesDepartmentComment;
            set => SetField(ref receivablesDepartmentComment, value);
        }
		
        private string salesDepartmentComment;
        /// <summary>
        /// Комментарий отдела продаж
        /// </summary>
        public virtual string SalesDepartmentComment {
            get => salesDepartmentComment;
            set => SetField(ref salesDepartmentComment, value);
        }

        public WorkingOnOrderViewModel(OrderBase order)
        {
            Order = order;
            UoW = UnitOfWorkFactory.CreateWithoutRoot();
            nonReturnReasons = UoW.Session.QueryOver<NonReturnReason>().List();
        }
    }
}
