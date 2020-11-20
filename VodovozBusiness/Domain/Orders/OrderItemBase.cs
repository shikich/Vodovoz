using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders
{
    public abstract class OrderItemBase : DomainObjectBase
    {
        private OrderBase order;
        [Display(Name = "Заказ")]
        public virtual OrderBase Order
        {
            get => order;
            set => SetField(ref order, value);
        }

        private decimal count;
        [Display(Name = "Количество")]
        public virtual decimal Count
        {
            get => count;
            set => SetField(ref count, value);
        }
        
        private decimal? actualCount;
        [Display(Name = "Фактическое количество")]
        public virtual decimal? ActualCount
        {
            get => actualCount;
            set => SetField(ref actualCount, value);
        }

        public virtual decimal CurrentCount => ActualCount ?? Count;
    }
}