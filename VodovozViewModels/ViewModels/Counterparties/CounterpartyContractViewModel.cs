using System;
using System.ComponentModel.DataAnnotations;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Services;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Client;
using Vodovoz.EntityRepositories;
using Vodovoz.Models;

namespace Vodovoz.ViewModels.ViewModels.Counterparties
{
    public class CounterpartyContractViewModel : DialogViewModelBase, IEditableDialog, IContractSaved
    {
	    private readonly ICommonServices commonServices;
	    private readonly IUnitOfWorkGeneric<CounterpartyContract> UoWGeneric;

		public event EventHandler<ContractSavedEventArgs> ContractSaved;

		public CounterpartyContract Entity => UoWGeneric.Root;
		public IUnitOfWork UoW => UoWGeneric;
		public ValidationContext ValidationContext { get; set; }
		
		private bool organisationSensitivity;
		public bool OrganisationSensitivity
		{
			get => organisationSensitivity;
			set => SetField(ref organisationSensitivity, value);
		}

		public bool IsEditable { get; set; } = true;

		public CounterpartyContractViewModel(
			Counterparty counterparty, 
			ICommonServices commonServices,
			INavigationManager navigationManager) : base(navigationManager)
		{
			this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			UoWGeneric = CounterpartyContract.Create(counterparty);
			ValidationContext = new ValidationContext(Entity);

			Title = "Новый договор";
		}

		/// <summary>
		/// Новый договор с заполненной организацией.
		/// </summary>
		public CounterpartyContractViewModel(
			Counterparty counterparty, 
			Domain.Organizations.Organization organization,
			ICommonServices commonServices,
			INavigationManager navigationManager) : this (counterparty, commonServices, navigationManager)
		{
			UoWGeneric.Root.Organization = organization;
			OrganisationSensitivity = false;
			Entity.UpdateContractTemplate(UoW);
		}

		public CounterpartyContractViewModel(
			Counterparty counterparty, 
			PaymentType paymentType, 
			Domain.Organizations.Organization organization, 
			DateTime? date,
			ICommonServices commonServices,
			INavigationManager navigationManager) : this(counterparty, organization, commonServices, navigationManager)
		{
			var orderOrganizationProviderFactory = new OrderOrganizationProviderFactory();
			var orderOrganizationProvider = orderOrganizationProviderFactory.CreateOrderOrganizationProvider();
			var counterpartyContractRepository = new CounterpartyContractRepository(orderOrganizationProvider);
			var contractType =  counterpartyContractRepository.GetContractTypeForPaymentType(counterparty.PersonType, paymentType);
			Entity.ContractType = contractType;
			
			if(date.HasValue)
				UoWGeneric.Root.IssueDate = date.Value;
		}

		public CounterpartyContractViewModel(
			int counterpartyContractId,
			ICommonServices commonServices,
			INavigationManager navigationManager) : base(navigationManager)
		{
			this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<CounterpartyContract>(counterpartyContractId);
			ValidationContext = new ValidationContext(Entity);
		}

		private DelegateCommand saveCommand;
		public DelegateCommand SaveCommand => saveCommand ?? (
			saveCommand = new DelegateCommand(
				() =>
				{
					if (Validate())
					{
						Save();
						Close(false, CloseSource.Save);
					}
				},
				() => true
			)
		);

		private void Save()
		{
			UoWGeneric.Save();
			ContractSaved?.Invoke(this, new ContractSavedEventArgs(UoWGeneric.Root));
		}

		private bool Validate() => commonServices.ValidationService.Validate(Entity, ValidationContext);
    }
}