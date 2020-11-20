using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders
{
    public enum MovementReason
    {
        [Display(Name = "Неизвестна")] Unknown,
        [Display(Name = "Сервис")] Service,
        [Display(Name = "Аренда")] Rent,
        [Display(Name = "Расторжение")] Cancellation,
        [Display(Name = "Продажа")] Sale
    }
    
    public class MovementReasonStringType : NHibernate.Type.EnumStringType
    {
        public MovementReasonStringType() : base(typeof(MovementReason)) { }
    }
}