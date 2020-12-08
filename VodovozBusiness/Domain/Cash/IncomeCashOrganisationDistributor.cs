using System;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Documents;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Operations;
using Vodovoz.Services;

namespace Vodovoz.Domain.Cash
{
    public class IncomeCashOrganisationDistributor
    {
        private readonly ICashDistributionCommonOrganisationProvider cashDistributionCommonOrganisationProvider;

        public IncomeCashOrganisationDistributor(ICashDistributionCommonOrganisationProvider cashDistributionCommonOrganisationProvider)
        {
            this.cashDistributionCommonOrganisationProvider =
                cashDistributionCommonOrganisationProvider ?? throw new ArgumentNullException(nameof(cashDistributionCommonOrganisationProvider));
        }

        public void DistributeCashForIncome(IUnitOfWork uow, Income income)
        {
            var org = cashDistributionCommonOrganisationProvider.GetCommonOrganisation(uow);
            var operation = CreateOrganisationCashMovementOperation(income, org);
            var incomeCashDistributionDoc = CreateIncomeCashDistributionDocument(income, operation);
            Save(operation, incomeCashDistributionDoc, uow);
        }
        
        public void UpdateRecords(IUnitOfWork uow, Income income, Employee editor)
        {
            var incomeCashDistributionDoc = 
                uow.Session.QueryOver<IncomeCashDistributionDocument>()
                           .Where(x => x.Income.Id == income.Id)
                           .SingleOrDefault();

            if (incomeCashDistributionDoc == null) return;
            
            UpdateIncomeCashDistributionDocument(incomeCashDistributionDoc, income, editor);
            UpdateOrganisationCashMovementOperation(incomeCashDistributionDoc.OrganisationCashMovementOperation, income);
            Save(incomeCashDistributionDoc.OrganisationCashMovementOperation, incomeCashDistributionDoc, uow);
        }

        private void Save(OrganisationCashMovementOperation operation, IncomeCashDistributionDocument document, IUnitOfWork uow)
        {
            uow.Save(operation);
            uow.Save(document);
        }

        private IncomeCashDistributionDocument CreateIncomeCashDistributionDocument(Income income, 
            OrganisationCashMovementOperation operation)
        {
            return new IncomeCashDistributionDocument
            {
                Author = income.Casher,
                Income = income,
                Organisation = operation.Organisation,
                CreationDate = DateTime.Now,
                LastEditor = income.Casher,
                CashIncomeCategory = income.IncomeCategory,
                LastEditedTime = DateTime.Now,
                OrganisationCashMovementOperation = operation,
                Amount = operation.Amount
            };
        }

        private OrganisationCashMovementOperation CreateOrganisationCashMovementOperation(Income income, Organization org)
        {
            return new OrganisationCashMovementOperation
            {
                Organisation = org,
                OperationTime = DateTime.Now,
                Amount = income.Money
            };
        }

        private void UpdateIncomeCashDistributionDocument(IncomeCashDistributionDocument doc, Income income, Employee editor)
        {
            doc.LastEditor = editor;
            doc.LastEditedTime = DateTime.Now;
            doc.Amount = income.Money;
        }

        private void UpdateOrganisationCashMovementOperation(OrganisationCashMovementOperation operation, Income income)
        {
            operation.Amount = income.Money;
            operation.OperationTime = DateTime.Now;
        }
    }
}