using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders
{
    public enum EquipmentOwnType
    {
        [Display(Name = "")] None,
        [Display(Name = "Клиент")] Client,
        [Display(Name = "Дежурный")] Duty,
        [Display(Name = "Аренда")] Rent
    }
    
    public class EquipmentOwnTypeStringType : NHibernate.Type.EnumStringType
    {
        public EquipmentOwnTypeStringType() : base(typeof(EquipmentOwnType)) { }
    }
}