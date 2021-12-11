﻿using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.ViewModels;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.Parameters;
using Vodovoz.EntityRepositories;
using Vodovoz.Services;
using Vodovoz.ViewModels.Journals.JournalFactories;
using QS.Project.Journal.EntitySelector;

namespace Vodovoz.ViewModels.ViewModels.Contacts
{
	public class PhonesViewModel : WidgetViewModelBase
	{
		#region Properties

		public IList<PhoneType> PhoneTypes;
		public event Action PhonesListReplaced; //Убрать
		public DeliveryPoint DeliveryPoint { get; set; }

		private GenericObservableList<Phone> phonesList ;
		public virtual GenericObservableList<Phone> PhonesList {
			get => phonesList;
			set {
				SetField(ref phonesList, value, () => PhonesList);
				PhonesListReplaced?.Invoke();
			} 
		}

		private bool readOnly = false;
		public virtual bool ReadOnly {
			get => readOnly;
			set => SetField(ref readOnly, value, () => ReadOnly);
		}

		public PhonesViewModel(IPhoneRepository phoneRepository, IUnitOfWork uow, IContactsParameters contactsParameters,
			IRoboAtsCounterpartyJournalFactory roboAtsCounterpartyJournalFactory)
		{
			this.phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
			this.contactsParameters = contactsParameters ?? throw new ArgumentNullException(nameof(contactsParameters));
			if(roboAtsCounterpartyJournalFactory == null)
			{
				throw new ArgumentNullException(nameof(roboAtsCounterpartyJournalFactory));
			}
			RoboAtsCounterpartyNameSelectorFactory = roboAtsCounterpartyJournalFactory.CreateRoboAtsCounterpartyNameAutocompleteSelectorFactory();
			RoboAtsCounterpartyPatronymicSelectorFactory = roboAtsCounterpartyJournalFactory.CreateRoboAtsCounterpartyPatronymicAutocompleteSelectorFactory();
			PhoneTypes = phoneRepository.GetPhoneTypes(uow);
			CreateCommands();
		}

		IContactsParameters contactsParameters;

		public IEntityAutocompleteSelectorFactory RoboAtsCounterpartyNameSelectorFactory { get; }
		public IEntityAutocompleteSelectorFactory RoboAtsCounterpartyPatronymicSelectorFactory { get; }

		IPhoneRepository phoneRepository;

		#endregion Prorerties

		#region Commands

		public DelegateCommand AddItemCommand { get; private set; }
		public DelegateCommand<Phone> DeleteItemCommand { get; private set; }

		private void CreateCommands()
		{
			AddItemCommand = new DelegateCommand(
				() => {
					var phone = new Phone().Init(ContactParametersProvider.Instance);
					phone.DeliveryPoint = DeliveryPoint;
					if(PhonesList == null)
						PhonesList = new GenericObservableList<Phone>();
					PhonesList.Add(phone);
				},
				() => { return !ReadOnly; }
			);

			DeleteItemCommand = new DelegateCommand<Phone>(
				(phone) => {
					PhonesList.Remove(phone);
				},
				(phone) => { return !ReadOnly;}
			);
		}

		#endregion Commands


		/// <summary>
		/// Необходимо выполнить перед сохранением или в геттере HasChanges
		/// </summary>
		public void RemoveEmpty()
		{
			PhonesList.Where(p => p.DigitsNumber.Length < contactsParameters.MinSavePhoneLength)
					.ToList().ForEach(p => PhonesList.Remove(p));
		}

	}
}
