using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private string _receivablesDepartmentComment;
        private string _salesDepartmentComment;

        public readonly IEnumerable<NonReturnReason> nonReturnReasons;

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
            
            Order.PropertyChanged += OrderOnPropertyChanged;
        }
        
        public OrderBase Order { get; set; }

        /// <summary>
        /// Комментарий отдела дебиторской задолженности
        /// </summary>
        public virtual string ReceivablesDepartmentComment {
            get => _receivablesDepartmentComment;
            set => SetField(ref _receivablesDepartmentComment, value);
        }
        
        /// <summary>
        /// Комментарий отдела продаж
        /// </summary>
        public virtual string SalesDepartmentComment {
            get => _salesDepartmentComment;
            set => SetField(ref _salesDepartmentComment, value);
        }

        //TODO Расширить свойтсво
        public bool CanEditOrder => Order.Status == OrderStatus.NewOrder;
        public bool CanShowComments => Order.Counterparty?.IsChainStore ?? false;
        public bool CanEditReceivablesDepartmentComment { get; }
        public bool CanEditSalesDepartmentComment { get; }
        
        private void OrderOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(Order.Counterparty):
                    OnPropertyChanged(nameof(CanShowComments));
                    break;
            }
        }
        
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
