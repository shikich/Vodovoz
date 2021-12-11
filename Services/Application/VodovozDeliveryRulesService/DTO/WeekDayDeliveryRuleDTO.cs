using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VodovozDeliveryRulesService
{
    [DataContract]
    public class WeekDayDeliveryRuleDTO
    {
        private DeliveryRulesService.WeekDayName weekDayEnum;
        public DeliveryRulesService.WeekDayName WeekDayEnum {
            get => weekDayEnum;
            set {
                weekDayEnum = value;
                WeekDay = weekDayEnum.ToString();
            }
        }

        [DataMember]
        public string WeekDay { get; set; }
		
        [DataMember]
        public IList<string> DeliveryRules { get; set; }
		
        [DataMember]
        public IList<string> ScheduleRestrictions { get; set; }
    }
}
