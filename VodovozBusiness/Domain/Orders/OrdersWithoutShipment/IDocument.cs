﻿using QS.DomainModel.Entity;
using Vodovoz.Domain.Orders.Documents;

namespace Vodovoz.Domain.Orders.OrdersWithoutShipment
{
	public interface IDocument : IDomainObject
	{
		Order Order { get; set; }
		OrderDocumentType Type { get; }
	}
}
