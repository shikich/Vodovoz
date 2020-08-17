using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Cash;
using Vodovoz.Services;

namespace Vodovoz.EntityRepositories.Cash
{
	public class CashCategoryRepository : ICashCategoryRepository
	{
		private readonly ICashCategoryParametersProvider cashCategoryParametersProvider;

		public CashCategoryRepository(ICashCategoryParametersProvider cashCategoryParametersProvider)
		{
			this.cashCategoryParametersProvider = cashCategoryParametersProvider ?? throw new ArgumentNullException(nameof(cashCategoryParametersProvider));
		}

		public IncomeCategory DefaultIncomeCategory(IUnitOfWork uow)
		{
			return uow.GetById<IncomeCategory>(cashCategoryParametersProvider.DefaultIncomeCategory);
		}

		public ExpenseCategory EmployeeSalaryExpenseCategory(IUnitOfWork uow)
		{
			return uow.GetById<ExpenseCategory>(cashCategoryParametersProvider.EmployeeSalaryExpenseCategory);
		}

		public IList<ExpenseCategory> ExpenseCategories(IUnitOfWork uow)
		{
			return uow.Session.QueryOver<ExpenseCategory>().OrderBy(ec => ec.Name).Asc().List();
		}

		public QueryOver<ExpenseCategory> ExpenseCategoriesQuery()
		{
			return QueryOver.Of<ExpenseCategory>().OrderBy(ec => ec.Name).Asc();
		}

		public IList<ExpenseCategory> ExpenseSelfDeliveryCategories(IUnitOfWork uow)
		{
			return uow.Session.QueryOver<ExpenseCategory>()
				.Where(x => x.ExpenseDocumentType == ExpenseInvoiceDocumentType.ExpenseInvoiceSelfDelivery)
				.OrderBy(ec => ec.Name).Asc()
				.List();
		}

		public ExpenseCategory FuelDocumentExpenseCategory(IUnitOfWork uow)
		{
			return uow.GetById<ExpenseCategory>(cashCategoryParametersProvider.FuelDocumentExpenseCategory);
		}

		public IList<IncomeCategory> IncomeCategories(IUnitOfWork uow)
		{
			return uow.Session.QueryOver<IncomeCategory>().OrderBy(ic => ic.Name).Asc().List();
		}

		public QueryOver<IncomeCategory> IncomeCategoriesQuery()
		{
			return QueryOver.Of<IncomeCategory>().OrderBy(ic => ic.Name).Asc();
		}

		public ExpenseCategory RouteListClosingExpenseCategory(IUnitOfWork uow)
		{
			return uow.GetById<ExpenseCategory>(cashCategoryParametersProvider.RouteListClosingExpenseCategory);
		}

		public IncomeCategory RouteListClosingIncomeCategory(IUnitOfWork uow)
		{
			return uow.GetById<IncomeCategory>(cashCategoryParametersProvider.RouteListClosingIncomeCategory);
		}

		public IList<IncomeCategory> SelfDeliveryIncomeCategories(IUnitOfWork uow)
		{
			return uow.Session.QueryOver<IncomeCategory>()
				.Where(x => x.IncomeDocumentType == IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery)
				.OrderBy(ic => ic.Name).Asc()
				.List();
		}
	}
}
