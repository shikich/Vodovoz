﻿using DriverAPI.Library.DTOs;
using Vodovoz.Domain.Client;

namespace DriverAPI.Library.Converters
{
	public class DeliveryPointConverter
	{
		public AddressDto extractAPIAddressFromDeliveryPoint(DeliveryPoint deliveryPoint)
		{
			return new AddressDto()
			{
				City = deliveryPoint.City,
				Street = deliveryPoint.Street,
				Building = deliveryPoint.Building + deliveryPoint.Letter,
				Entrance = deliveryPoint.Entrance,
				Floor = deliveryPoint.Floor,
				Apartment = deliveryPoint.Room,
				DeliveryPointCategory = deliveryPoint.Category?.Name,
				EntranceType = deliveryPoint.EntranceType.ToString(),
				RoomType = deliveryPoint.RoomType.ToString(),
				Latitude = deliveryPoint.ActiveVersion.Latitude ?? 0,
				Longitude = deliveryPoint.ActiveVersion.Longitude ?? 0
			};
		}
	}
}
