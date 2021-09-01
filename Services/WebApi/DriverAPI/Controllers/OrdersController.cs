﻿using DriverAPI.DTOs;
using DriverAPI.Library.DTOs;
using DriverAPI.Library.Helpers;
using DriverAPI.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrdersController : ControllerBase
	{
		private readonly ILogger<OrdersController> _logger;
		private readonly IEmployeeModel _employeeData;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IOrderModel _aPIOrderData;
		private readonly IActionTimeHelper _actionTimeHelper;

		public OrdersController(
			ILogger<OrdersController> logger,
			IEmployeeModel employeeData,
			UserManager<IdentityUser> userManager,
			IOrderModel aPIOrderData,
			IActionTimeHelper actionTimeHelper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_employeeData = employeeData ?? throw new ArgumentNullException(nameof(employeeData));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_aPIOrderData = aPIOrderData ?? throw new ArgumentNullException(nameof(aPIOrderData));
			_actionTimeHelper = actionTimeHelper;
		}

		/// <summary>
		/// Эндпоинт получения информации о заказе
		/// В ответе сервера будет JSON объект с полями соответствующими APIOrder
		/// </summary>
		/// <param name="orderId">Идентификатор заказа</param>
		[HttpGet]
		[Route("/api/GetOrder")]
		public OrderDto Get(int orderId)
		{
			return _aPIOrderData.Get(orderId);
		}

		// POST: CompleteOrderDelivery / CompleteRouteListAddress
		[HttpPost]
		[Route("/api/CompleteOrderDelivery")]
		public void CompleteOrderDelivery([FromBody] CompletedOrderRequestDto completedOrderRequestModel)
		{
			_logger.LogInformation($"Завершение заказа: { completedOrderRequestModel.OrderId } пользователем {HttpContext.User.Identity?.Name ?? "Unknown"}");

			var user = _userManager.GetUserAsync(User).Result;
			var driver = _employeeData.GetByAPILogin(user.UserName);

			var recievedTime = DateTime.Now;

			_actionTimeHelper.ThrowIfNotValid(recievedTime, completedOrderRequestModel.ActionTime);

			_aPIOrderData.CompleteOrderDelivery(
				driver,
				completedOrderRequestModel.OrderId,
				completedOrderRequestModel.BottlesReturnCount,
				completedOrderRequestModel.Rating,
				completedOrderRequestModel.DriverComplaintReasonId,
				completedOrderRequestModel.OtherDriverComplaintReasonComment,
				recievedTime
			);
		}

		/// <summary>
		/// Эндпоинт смены типа оплаты заказа
		/// </summary>
		/// <param name="changeOrderPaymentTypeRequestModel">Модель данных входящего запроса</param>
		[HttpPost]
		[Route("/api/ChangeOrderPaymentType")]
		public void ChangeOrderPaymentType(ChangeOrderPaymentTypeRequestDto changeOrderPaymentTypeRequestModel)
		{
			var recievedTime = DateTime.Now;

			var user = _userManager.GetUserAsync(User).Result;
			var driver = _employeeData.GetByAPILogin(user.UserName);

			var orderId = changeOrderPaymentTypeRequestModel.OrderId;
			var newPaymentType = changeOrderPaymentTypeRequestModel.NewPaymentType;

			_logger.LogInformation($"Смена типа оплаты заказа: { orderId } на { newPaymentType }" +
				$" на стороне приложения в { changeOrderPaymentTypeRequestModel.ActionTime } пользователем {HttpContext.User.Identity?.Name ?? "Unknown"}");

			_actionTimeHelper.ThrowIfNotValid(recievedTime, changeOrderPaymentTypeRequestModel.ActionTime);

			IEnumerable<PaymentDtoType> availableTypesToChange = _aPIOrderData.GetAvailableToChangePaymentTypes(orderId);

			if (!availableTypesToChange.Contains(newPaymentType))
			{
				var errorMessage = $"Попытка сменить тип оплаты у заказа { orderId } на недоступный для этого заказа тип оплаты { newPaymentType }";
				_logger.LogWarning(errorMessage);
				throw new ArgumentOutOfRangeException(nameof(changeOrderPaymentTypeRequestModel.NewPaymentType), errorMessage);
			}

			Vodovoz.Domain.Client.PaymentType newVodovozPaymentType;

			if (newPaymentType == PaymentDtoType.Terminal)
			{
				newVodovozPaymentType = Vodovoz.Domain.Client.PaymentType.Terminal;
			}
			else if (newPaymentType == PaymentDtoType.Cash)
			{
				newVodovozPaymentType = Vodovoz.Domain.Client.PaymentType.cash;
			}
			else
			{
				var errorMessage = $"Попытка сменить тип оплаты у заказа { orderId } на не поддерживаемый для смены тип оплаты { newPaymentType }";
				_logger.LogWarning(errorMessage);
				throw new ArgumentOutOfRangeException(nameof(changeOrderPaymentTypeRequestModel.NewPaymentType), errorMessage);
			}

			_aPIOrderData.ChangeOrderPaymentType(orderId, newVodovozPaymentType, driver);
		}
	}
}
