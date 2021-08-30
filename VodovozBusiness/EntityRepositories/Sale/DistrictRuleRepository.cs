﻿using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Sale;
using Vodovoz.Domain.Sectors;

namespace Vodovoz.EntityRepositories.Sale
{
	public class DistrictRuleRepository : IDistrictRuleRepository
	{
		public QueryOver<DeliveryPriceRule> GetQueryOverWithAllDeliveryPriceRules()
		{
			var res = QueryOver.Of<DeliveryPriceRule>();
			return res;
		}

		public IList<DeliveryPriceRule> GetAllDeliveryPriceRules(IUnitOfWork uow)
		{
			var res = GetQueryOverWithAllDeliveryPriceRules().GetExecutableQueryOver(uow.Session).List();
			return res;
		}
		
		public IList<CommonSectorsRuleItem> GetCommonDistrictRuleItemsForDistrict(IUnitOfWork uow, SectorDeliveryRuleVersion deliveryRuleVersion)
		{
			var res = uow.Session.QueryOver<CommonSectorsRuleItem>()
				.Where(i => i.SectorDeliveryRuleVersion.Id == deliveryRuleVersion.Id)
				.List();
			return res;
		}
		
		public IList<SectorVersion> GetSectorsHavingRule(IUnitOfWork uow, DeliveryPriceRule rule)
		{
			SectorVersion sectorVersionAlias = null;
			CommonSectorsRuleItem commonSectorsRuleItemAlias = null;
			SectorDeliveryRuleVersion sectorDeliveryRuleVersionAlias = null;
			var res = uow.Session.QueryOver(() => commonSectorsRuleItemAlias)
				.JoinAlias(() => sectorDeliveryRuleVersionAlias, () => commonSectorsRuleItemAlias.SectorDeliveryRuleVersion)
				.JoinEntityAlias(() => sectorVersionAlias,
					() => sectorVersionAlias.Sector.Id == sectorDeliveryRuleVersionAlias.Sector.Id &&
					      sectorVersionAlias.Status == SectorsSetStatus.Active,
					JoinType.LeftOuterJoin)
				.Where(d => d.DeliveryPriceRule.Id == rule.Id)
						 .List()
						 .Select(x => sectorVersionAlias)
						 .ToList();

			return res;
		}
	}
}
