﻿using DriverAPI.Library.Converters;
using DriverAPI.Library.Models;
using DriverAPI.Library.Helpers;
using DriverAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DriverAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class SmsPaymentsController : ControllerBase
	{
		private readonly ILogger<SmsPaymentsController> _logger;
		private readonly ISmsPaymentModel _aPISmsPaymentData;
		private readonly SmsPaymentStatusConverter _smsPaymentConverter;
		private readonly ISmsPaymentServiceAPIHelper _smsPaymentServiceAPIHelper;
		private readonly IOrderModel _aPIOrderData;

		public SmsPaymentsController(ILogger<SmsPaymentsController> logger,
			ISmsPaymentModel aPISmsPaymentData,
			SmsPaymentStatusConverter smsPaymentConverter,
			ISmsPaymentServiceAPIHelper smsPaymentServiceAPIHelper,
			IOrderModel aPIOrderData)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_aPISmsPaymentData = aPISmsPaymentData ?? throw new ArgumentNullException(nameof(aPISmsPaymentData));
			_smsPaymentConverter = smsPaymentConverter ?? throw new ArgumentNullException(nameof(smsPaymentConverter));
			_smsPaymentServiceAPIHelper = smsPaymentServiceAPIHelper ?? throw new ArgumentNullException(nameof(smsPaymentServiceAPIHelper));
			_aPIOrderData = aPIOrderData ?? throw new ArgumentNullException(nameof(aPIOrderData));
		}

		/// <summary>
		/// Эндпоинт получения статуса оплаты через СМС
		/// </summary>
		/// <param name="orderId">Идентификатор заказа</param>
		/// <returns>OrderPaymentStatusResponseModel или null</returns>
		[HttpGet]
		[Route("/api/GetOrderSmsPaymentStatus")]
		public OrderPaymentStatusResponseDto GetOrderSmsPaymentStatus(int orderId)
		{
			var additionalInfo = _aPIOrderData.GetAdditionalInfo(orderId)
				?? throw new Exception($"Не удалось получить информацию о заказе {orderId}");

			var response = new OrderPaymentStatusResponseDto()
			{
				AvailablePaymentTypes = additionalInfo.AvailablePaymentTypes,
				CanSendSms = additionalInfo.CanSendSms,
				SmsPaymentStatus = _smsPaymentConverter.convertToAPIPaymentStatus(
					_aPISmsPaymentData.GetOrderPaymentStatus(orderId)
				)
			};

			return response;
		}

		/// <summary>
		/// Эндпоинт запроса СМС для оплаты заказа
		/// </summary>
		/// <param name="payBySmsRequestModel"></param>
		[HttpPost]
		[Route("/api/PayBySms")]
		public void PayBySms(PayBySmsRequestDto payBySmsRequestModel)
		{
			_logger.LogInformation($"Запрос смены оплаты заказа: { payBySmsRequestModel.OrderId } на оплату по СМС с номером { payBySmsRequestModel.PhoneNumber } пользователем {HttpContext.User.Identity?.Name ?? "Unknown"}");

			_smsPaymentServiceAPIHelper.SendPayment(payBySmsRequestModel.OrderId, payBySmsRequestModel.PhoneNumber).Wait();
		}
	}
}
