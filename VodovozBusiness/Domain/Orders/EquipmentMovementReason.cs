using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders
{
    public enum EquipmentMovementReason
    {
        [Display(Name = "")] None, 
        [Display(Name = "Аренда")] Rent, 
        [Display(Name = "Ремонт")] Repair, 
        [Display(Name = "Санобработка")] Cleaning, 
        [Display(Name = "Ремонт и санобработка")] RepairAndCleaning
    }
    
    public class EquipmentMovementReasonStringType : NHibernate.Type.EnumStringType
    {
        public EquipmentMovementReasonStringType() : base(typeof(EquipmentMovementReason)) { }
    }
}