using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders {
    public enum OrderType {
        [Display(Name = "Заказ-самовывоз")]
        SelfDeliveryOrder,
        [Display(Name = "Заказ-доставка")]
        DeliveryOrder,
        [Display(Name = "Сервисный заказ")]
        VisitingMasterOrder,
        [Display(Name = "Заказ-закрытие документов")]
        ClosingDocOrder,
        [Display(Name = "Заказ из 1с")]
        OrderFrom1c
    }
}