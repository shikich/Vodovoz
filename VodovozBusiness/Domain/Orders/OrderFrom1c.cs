using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Orders {
    public class OrderFrom1c : OrderBase {
        int? returnedTare;
        [Display(Name = "Возвратная тара")]
        public virtual int? ReturnedTare {
            get => returnedTare;
            set => SetField(ref returnedTare, value);
        }
        
        private string code1c;
        [Display(Name = "Код 1С")]
        public virtual string Code1c {
            get => code1c;
            set => SetField(ref code1c, value);
        }

        private string address1c;
        [Display(Name = "Адрес 1С")]
        public virtual string Address1c {
            get => address1c;
            set => SetField(ref address1c, value);
        }

        private string address1cCode;
        [Display(Name = "Код адреса 1С")]
        public virtual string Address1cCode {
            get => address1cCode;
            set => SetField(ref address1cCode, value);
        }
        
        private string deliverySchedule1c;
        [Display(Name = "Время доставки из 1С")]
        public virtual string DeliverySchedule1c {
            get => string.IsNullOrWhiteSpace(deliverySchedule1c) ? "Время доставки из 1С не загружено" : deliverySchedule1c;
            set => SetField(ref deliverySchedule1c, value);
        }
        
        string clientPhone;
        [Display(Name = "Номер телефона")]
        public virtual string ClientPhone {
            get => clientPhone;
            set => SetField(ref clientPhone, value);
        }
        
        private string toClientText;
        [Display(Name = "Оборудование к клиенту")]
        public virtual string ToClientText {
            get => toClientText;
            set => SetField(ref toClientText, value);
        }
        
        private string fromClientText;
        [Display(Name = "Оборудование от клиента")]
        public virtual string FromClientText {
            get => fromClientText;
            set => SetField(ref fromClientText, value);
        }

        public override DefaultOrderType DefaultOrderType => DefaultOrderType.OrderFrom1c;
    }
}