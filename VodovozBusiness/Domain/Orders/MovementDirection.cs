using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders
{
    public enum MovementDirection
    {
        [Display(Name = "Доставка")] Deliver,
        [Display(Name = "Забор")] PickUp
    }
    
    public class MovementDirectionStringType : NHibernate.Type.EnumStringType
    {
        public MovementDirectionStringType() : base(typeof(MovementDirection)) { }
    }
}