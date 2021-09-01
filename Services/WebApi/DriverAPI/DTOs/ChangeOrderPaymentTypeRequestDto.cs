﻿using DriverAPI.Library.DTOs;
using System;

namespace DriverAPI.DTOs
{
	public class ChangeOrderPaymentTypeRequestDto : IDelayedAction
	{
		public int OrderId { get; set; }
		public PaymentDtoType NewPaymentType { get; set; }
		public DateTime ActionTime { get; set; }
	}
}
