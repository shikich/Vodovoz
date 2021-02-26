﻿using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories;
using Vodovoz.Models;
using Vodovoz.Repository.Client;

namespace Vodovoz.Repositories.Client
{
	public class CounterpartyContractFactory
	{
		private readonly IOrganizationProvider organizationProvider;
		private readonly ICounterpartyContractRepository counterpartyContractRepository;

		public CounterpartyContractFactory(IOrganizationProvider organizationProvider, ICounterpartyContractRepository counterpartyContractRepository)
		{
			this.organizationProvider = organizationProvider ?? throw new ArgumentNullException(nameof(organizationProvider));
			this.counterpartyContractRepository = counterpartyContractRepository ?? throw new ArgumentNullException(nameof(counterpartyContractRepository));
		}
		
		public CounterpartyContract CreateContract(IUnitOfWork uow, Order order, DateTime? issueDate)
		{
			var contractType = counterpartyContractRepository.GetContractTypeForPaymentType(order.Counterparty.PersonType, order.PaymentType);
			var org = organizationProvider.GetOrganization(uow, order);
			var contractSubNumber = CounterpartyContract.GenerateSubNumber(order.Counterparty);
			
			CounterpartyContract contract = new CounterpartyContract {
				 Counterparty = order.Counterparty,
				 ContractSubNumber = contractSubNumber,
				 Organization = org,
				 IsArchive = false,
				 ContractType = contractType
			};
			if(issueDate.HasValue) {
				contract.IssueDate = issueDate.Value;
			}
			return contract;
		}
	}
}
