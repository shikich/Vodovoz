using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using NLog;

namespace VodovozDeliveryRulesService
{
	public class DeliveryRulesService : IDeliveryRulesService
	{
		
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public DeliveryRulesDTO GetRulesByDistrict(decimal latitude, decimal longitude)
		{
			try
			{
				var r = new DeliveryRulesDTO();
				r.StatusEnum = DeliveryRulesResponseStatus.Ok;
				r.Message = "";
				r.WeekDayDeliveryRules = new List<WeekDayDeliveryRuleDTO>();

				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Today,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string>()
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Monday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Tuesday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Wednesday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Thursday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Friday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Saturday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});
				r.WeekDayDeliveryRules.Add(new WeekDayDeliveryRuleDTO
				{
					WeekDayEnum = WeekDayName.Sunday,
					DeliveryRules = new List<string> {"Если 19л б. < 2шт. или 6л б. < 20шт. или 1500мл б. < 36шт. или 600мл б. < 96шт. или 500мл б. < 96шт., то цена 300 ₽" },
					ScheduleRestrictions = new List<string> {"9-22"}
				});

				return r;
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				return new DeliveryRulesDTO();
			}
		}
		
		public DeliveryInfoDTO GetDeliveryInfo(decimal latitude, decimal longitude)
		{
			var r = new DeliveryInfoDTO();
			r.StatusEnum = DeliveryRulesResponseStatus.Ok;
			r.Message = "";

			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO()
			{
				WeekDayEnum = WeekDayName.Today,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string>()
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Monday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Tuesday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Wednesday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Thursday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Friday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Saturday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});
			r.WeekDayDeliveryInfos.Add(new WeekDayDeliveryInfoDTO
			{
				WeekDayEnum = WeekDayName.Sunday,
				DeliveryRules = new List<DeliveryRuleDTO>
				{
					new DeliveryRuleDTO
					{
						Bottles19l = "2",
						Bottles6l = "20",
						Bottles1500ml = "36",
						Bottles600ml = "96",
						Bottles500ml = "96",
						Price = "300"
					}
				},
				ScheduleRestrictions = new List<string> {"9-22"}
			});

			return r;
		}

		public bool ServiceStatus()
		{
			var response = GetDeliveryInfo(59.886134m, 30.394007m);
			var response2 = GetRulesByDistrict(59.886134m, 30.394007m);
			if(response.StatusEnum == DeliveryRulesResponseStatus.Error || response2.StatusEnum == DeliveryRulesResponseStatus.Error)
				return false;
			return true;
		}
		
		public enum WeekDayName
		{
			[Display(Name = "Сегодня")]
			Today = 0,
			[Display(Name = "Понедельник")]
			Monday = 1,
			[Display(Name = "Вторник")]
			Tuesday = 2,
			[Display(Name = "Среда")]
			Wednesday = 3,
			[Display(Name = "Четверг")]
			Thursday = 4,
			[Display(Name = "Пятница")]
			Friday = 5,
			[Display(Name = "Суббота")]
			Saturday = 6,
			[Display(Name = "Воскресенье")]
			Sunday = 7
		}
	}
}
