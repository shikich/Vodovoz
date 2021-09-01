﻿using DriverAPI.Library.DTOs;
using System;

namespace DriverAPI.DTOs
{
	public class RouteListAddressCoordinateDto : IDelayedAction
	{
		public int RouteListAddressId { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public DateTime ActionTime { get; set; }
		public ActionDtoType ActionType { get; set; }
	}
}
