using System;
namespace Vodovoz.Services
{
	public interface ICashCategoryParametersProvider
	{
		int DefaultIncomeCategory { get; }
		int RouteListClosingIncomeCategory { get; }
		int RouteListClosingExpenseCategory { get; }
		int FuelDocumentExpenseCategory { get; }
		int EmployeeSalaryExpenseCategory { get; }
	}
}
