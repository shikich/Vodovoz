using System;
using System.Collections.Generic;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class WorkingOnOrderViewModel : UoWWidgetViewModelBase
    {
        private readonly ICommonServices _commonServices;
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

        public WorkingOnOrderViewModel(
            OrderBase order,
            ICommonServices commonServices)
        {
            _commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
            Order = order;
            UoW = UnitOfWorkFactory.CreateWithoutRoot();
            nonReturnReasons = UoW.Session.QueryOver<NonReturnReason>().List();
            
            CanEditReceivablesDepartmentComment = 
                _commonServices.CurrentPermissionService.ValidatePresetPermission("can_change_odz_op_comment");
            CanEditSalesDepartmentComment = 
                _commonServices.CurrentPermissionService.ValidatePresetPermission("can_change_sales_department_comment");
        }
        
        //TODO Расширить свойтсво
        public bool CanEditOrder => Order.Status == OrderStatus.NewOrder;
        public bool CanShowComments => Order.Counterparty?.IsChainStore ?? false;
        public bool CanEditReceivablesDepartmentComment { get; }
        public bool CanEditSalesDepartmentComment { get; }
        
        public void OnEnumDiverCallTypeChanged(object sender, EventArgs e)
        {
            var listDriverCallType = UoW.Session.QueryOver<Order>()
                .Where(x => x.Id == Order.Id)
                .Select(x => x.DriverCallType).List<DriverCallType>().FirstOrDefault();

            if(listDriverCallType != Order.DriverCallType) 
            {
                var max = UoW.Session.QueryOver<Order>()
                                .Select(NHibernate.Criterion.Projections.Max<Order>(x => x.DriverCallId))
                                .SingleOrDefault<int>();
                Order.DriverCallNumber = max != 0 ? max + 1 : 1;
            }
        }
    }
}
