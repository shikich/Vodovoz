﻿using FluentNHibernate.Mapping;
using Vodovoz.Domain.FastPayments;

namespace Vodovoz.HibernateMapping.FastPayments
{
	public class FastPaymentMap : ClassMap<FastPayment>
	{
		public FastPaymentMap()
		{
			Table("fast_payments");

			Id(x => x.Id).Column("id").GeneratedBy.Native();
            
			Map(x => x.FastPaymentStatus).Column("payment_status").CustomType<FastPaymentStatusStringType>();
			Map(x => x.Amount).Column("amount");
			Map(x => x.CreationDate).Column("creation_date");
			Map(x => x.PaidDate).Column("paid_date");
			Map(x => x.Ticket).Column("ticket");
			Map(x => x.QRPngBase64).Column("qr_code");
			Map(x => x.ExternalId).Column("external_id");
			Map(x => x.PhoneNumber).Column("phone_number");
            
			References(x => x.Order).Column("order_id");
		}
	}
}
