using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Cash;
using Vodovoz.Parameters;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.Domain.Service.BaseParametersServices;

namespace Vodovoz.Repository.Cash
{
	[Obsolete("Устарел. Используйте CashCategoryRepository")]
	public class CategoryRepository
	{
		const string defaultIncomeCategory 			 = "default_income_category";
		const string routeListClosingIncomeCategory  = "routelist_income_category_id";
		const string routeListClosingExpenseCategory = "routelist_expense_category_id";
		const string fuelDocumentExpenseCategory 	 = "fuel_expense";
		const string employeeSalaryExpenseCategory   = "employee_salary"; 		// Параметр базы для статьи расхода для авансов.

		private static ICashCategoryRepository GetCategoryRepository()
		{
			return new CashCategoryRepository(new CashCategoryParametersProvider(new ParametersProvider()));
		}

		public static IList<IncomeCategory> IncomeCategories (IUnitOfWork uow)
		{
			return GetCategoryRepository().IncomeCategories(uow);
		}

		public static IList<IncomeCategory> SelfDeliveryIncomeCategories(IUnitOfWork uow)
		{
			return GetCategoryRepository().SelfDeliveryIncomeCategories(uow);
		}

		public static IList<ExpenseCategory> ExpenseCategories (IUnitOfWork uow)
		{
			return GetCategoryRepository().ExpenseCategories(uow);
		}

		public static IList<ExpenseCategory> ExpenseSelfDeliveryCategories(IUnitOfWork uow)
		{
			return GetCategoryRepository().ExpenseSelfDeliveryCategories(uow);
		}

		public static QueryOver<ExpenseCategory> ExpenseCategoriesQuery ()
		{
			return GetCategoryRepository().ExpenseCategoriesQuery();
		}

		public static QueryOver<IncomeCategory> IncomeCategoriesQuery ()
		{
			return GetCategoryRepository().IncomeCategoriesQuery();
		}

		public static IncomeCategory DefaultIncomeCategory (IUnitOfWork uow)
		{
			return GetCategoryRepository().DefaultIncomeCategory(uow);
		}

		public static IncomeCategory RouteListClosingIncomeCategory(IUnitOfWork uow)
		{
			return GetCategoryRepository().RouteListClosingIncomeCategory(uow);
		}

		public static ExpenseCategory RouteListClosingExpenseCategory(IUnitOfWork uow)
		{
			return GetCategoryRepository().RouteListClosingExpenseCategory(uow);
		}

		internal static Func<IUnitOfWork, ExpenseCategory> FuelDocumentExpenseCategoryTestGap;
		public static ExpenseCategory FuelDocumentExpenseCategory(IUnitOfWork uow)
		{
			if(FuelDocumentExpenseCategoryTestGap != null) {
				return FuelDocumentExpenseCategoryTestGap(uow);
			}

			return GetCategoryRepository().FuelDocumentExpenseCategory(uow);
		}

		public static ExpenseCategory EmployeeSalaryExpenseCategory(IUnitOfWork uow)
		{
			return GetCategoryRepository().EmployeeSalaryExpenseCategory(uow);
		}
	}
}

