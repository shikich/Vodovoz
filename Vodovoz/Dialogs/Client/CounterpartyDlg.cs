﻿using System;
using System.Collections.Generic;
using NLog;
using QSBanks;
using QSContacts;
using QSOrmProject;
using QSProjectsLib;
using QSValidation;
using Vodovoz.Domain.Cash;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Panel;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class CounterpartyDlg : OrmGtkDialogBase<Counterparty>, ICounterpartyInfoProvider
	{
		static Logger logger = LogManager.GetCurrentClassLogger ();

		public event EventHandler<CurrentObjectChangedArgs> CurrentObjectChanged;

		public PanelViewType[] InfoWidgets
		{
			get
			{
				return new[]
				{
					PanelViewType.CounterpartyView,
				};
			}
		}

		public Counterparty Counterparty
		{
			get
			{
				return UoWGeneric.Root;
			}
		}

		public CounterpartyDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Counterparty> ();
			TabName = "Новый контрагент";
			ConfigureDlg ();
		}

		public CounterpartyDlg (int id)
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Counterparty> (id);
			ConfigureDlg ();
		}

		public CounterpartyDlg (Counterparty sub) : this (sub.Id)
		{
		}

		private void ConfigureDlg ()
		{
			notebook1.CurrentPage = 0;
			notebook1.ShowTabs = false;
			//Initializing null fields
			emailsView.Session = UoWGeneric.Session;
			phonesView.UoW = UoWGeneric;
			if (UoWGeneric.Root.Emails == null)
				UoWGeneric.Root.Emails = new List<Email> ();
			emailsView.Emails = UoWGeneric.Root.Emails;
			if (UoWGeneric.Root.Phones == null)
				UoWGeneric.Root.Phones = new List<Phone> ();
			phonesView.Phones = UoWGeneric.Root.Phones;
			if (UoWGeneric.Root.CounterpartyContracts == null) {
				UoWGeneric.Root.CounterpartyContracts = new List<CounterpartyContract> ();
			}
			//Other fields properties
			validatedINN.ValidationMode = validatedKPP.ValidationMode = QSWidgetLib.ValidationType.numeric;
			validatedINN.Binding.AddBinding(Entity, e => e.INN, w => w.Text).InitializeFromSource();
			validatedKPP.Binding.AddBinding(Entity, e => e.KPP, w => w.Text).InitializeFromSource();

			yentrySignFIO.Binding.AddBinding(Entity, e => e.SignatoryFIO, w => w.Text).InitializeFromSource();
			yentrySignPost.Binding.AddBinding(Entity, e => e.SignatoryPost, w => w.Text).InitializeFromSource();
			yentrySignBaseOf.Binding.AddBinding(Entity, e => e.SignatoryBaseOf, w => w.Text).InitializeFromSource();

			entryFIO.Binding.AddBinding(Entity, e => e.Name, w => w.Text).InitializeFromSource();
			entryFullName.Binding.AddBinding(Entity, e => e.FullName, w => w.Text).InitializeFromSource();

			spinMaxCredit.Binding.AddBinding(Entity, e => e.MaxCredit, w => w.ValueAsDecimal).InitializeFromSource();

			dataComment.Binding.AddBinding(Entity, e => e.Comment, w => w.Buffer.Text).InitializeFromSource();
			dataWaybillComment.Binding.AddBinding(Entity, e => e.WaybillComment, w => w.Buffer.Text).InitializeFromSource();

			enumPayment.ItemsEnum = typeof(PaymentType);
			enumPayment.Binding.AddBinding(Entity, s => s.PaymentMethod, w => w.SelectedItemOrNull).InitializeFromSource();

			enumDefaultDocumentType.ItemsEnum = typeof(DefaultDocumentType);
			enumDefaultDocumentType.Binding.AddBinding(Entity, s => s.DefaultDocumentType, w => w.SelectedItemOrNull).InitializeFromSource();

			ycheckIsArchived.Binding.AddBinding(Entity, e => e.IsArchive, w => w.Active).InitializeFromSource();

			var counterpatiesView = new ViewModel.CounterpartyVM(UoW);
			referenceMainCounterparty.RepresentationModel = counterpatiesView;
			referenceMainCounterparty.Binding.AddBinding(Entity, e => e.MainCounterparty, w => w.Subject).InitializeFromSource();
			referencePreviousCounterparty.RepresentationModel = counterpatiesView;
			referencePreviousCounterparty.Binding.AddBinding(Entity, e => e.PreviousCounterparty, w => w.Subject).InitializeFromSource();

			checkCustomer.Binding.AddBinding(Entity, e => e.CooperationCustomer, w => w.Active).InitializeFromSource();
			checkSupplier.Binding.AddBinding(Entity, e => e.CooperationSupplier, w => w.Active).InitializeFromSource();
			checkPartner.Binding.AddBinding(Entity, e => e.CooperationPartner, w => w.Active).InitializeFromSource();

			//Setting subjects
			accountsView.ParentReference = new ParentReferenceGeneric<Counterparty, Account> (UoWGeneric, c => c.Accounts);
			deliveryPointView.DeliveryPointUoW = UoWGeneric;
			counterpartyContractsView.CounterpartyUoW = UoWGeneric;
			referenceSignificance.SubjectType = typeof(Significance);
			referenceSignificance.Binding.AddBinding(Entity, e => e.Significance, w => w.Subject).InitializeFromSource();
			referenceStatus.SubjectType = typeof(CounterpartyStatus);
			referenceStatus.Binding.AddBinding(Entity, e => e.Status, w => w.Subject).InitializeFromSource();
			referenceDefaultExpense.SubjectType = typeof(ExpenseCategory);
			referenceDefaultExpense.Binding.AddBinding(Entity, e => e.DefaultExpenseCategory, w => w.Subject).InitializeFromSource();
			referenceAccountant.SubjectType = referenceBottleManager.SubjectType = referenceSalesManager.SubjectType = typeof(Employee);
			proxiesview1.CounterpartyUoW = UoWGeneric;
			dataentryMainContact.RepresentationModel = new ViewModel.ContactsVM (UoW, Entity);
			dataentryMainContact.Binding.AddBinding(Entity, e => e.MainContact, w => w.Subject).InitializeFromSource();
			dataentryFinancialContact.RepresentationModel = new ViewModel.ContactsVM (UoW, Entity);
			dataentryFinancialContact.Binding.AddBinding(Entity, e => e.FinancialContact, w => w.Subject).InitializeFromSource();
			//Setting Contacts
			contactsview1.CounterpartyUoW = UoWGeneric;
			//Setting permissions
			spinMaxCredit.Sensitive = QSMain.User.Permissions ["max_loan_amount"];
			datalegalname1.Binding.AddSource (Entity)
				.AddBinding (s => s.Name, t => t.OwnName)
				.AddBinding (s => s.TypeOfOwnership, t => t.Ownership)
				.AddBinding (s => s.FullName, t => t.FullName)
				.InitializeFromSource ();

			//make actions menu
			var menu = new Gtk.Menu ();
			var menuItem = new Gtk.MenuItem ("Все заказы контрагента");
			menuItem.Activated += AllOrders_Activated;
			menu.Add (menuItem);
			menuActions.Menu = menu;
			menu.ShowAll ();
			menuActions.Sensitive = !UoWGeneric.IsNew;
		}

		void AllOrders_Activated (object sender, EventArgs e)
		{
			var filter = new OrdersFilter (UnitOfWorkFactory.CreateWithoutRoot ());
			filter.RestrictCounterparty = Entity;

			ReferenceRepresentation OrdersDialog = new ReferenceRepresentation (new ViewModel.OrdersVM (filter));
			OrdersDialog.Mode = OrmReferenceMode.Normal;

			TabParent.AddTab (OrdersDialog, this, false);
		}

		public override bool Save ()
		{
			Entity.UoW = UoW;
			var valid = new QSValidator<Counterparty> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;

			logger.Info ("Сохраняем контрагента...");
			phonesView.SaveChanges ();
			emailsView.SaveChanges ();
			UoWGeneric.Save ();
			logger.Info ("Ok.");
			return true;
		}

		protected void OnRadioInfoToggled (object sender, EventArgs e)
		{
			if (radioInfo.Active)
				notebook1.CurrentPage = 0;
		}

		protected void OnRadioContactsToggled (object sender, EventArgs e)
		{
			if (radioContacts.Active)
				notebook1.CurrentPage = 1;
		}

		protected void OnRadioDetailsToggled (object sender, EventArgs e)
		{
			if (radioDetails.Active)
				notebook1.CurrentPage = 2;
		}

		protected void OnRadioCuratorsToggled (object sender, EventArgs e)
		{
			if (radioCurators.Active)
				notebook1.CurrentPage = 3;
		}

		protected void OnRadioContactPersonsToggled (object sender, EventArgs e)
		{
			if (radioContactPersons.Active)
				notebook1.CurrentPage = 4;
		}

		protected void OnRadiobuttonProxiesToggled (object sender, EventArgs e)
		{
			if (radiobuttonProxies.Active)
				notebook1.CurrentPage = 5;
		}

		protected void OnRadioContractsToggled (object sender, EventArgs e)
		{
			if (radioContracts.Active)
				notebook1.CurrentPage = 6;
		}

		protected void OnRadioDeliveryPointToggled (object sender, EventArgs e)
		{
			if (radioDeliveryPoint.Active)
				notebook1.CurrentPage = 7;
		}

		protected void OnEnumPersonTypeChanged (object sender, EventArgs e)
		{
			labelFIO.Visible = entryFIO.Visible = Entity.PersonType == PersonType.natural;
			labelShort.Visible = datalegalname1.Visible = 
				labelFullName.Visible = entryFullName.Visible = 
					referenceMainCounterparty.Visible = labelMainCounterparty.Visible =
					radioDetails.Visible = radiobuttonProxies.Visible = Entity.PersonType == PersonType.legal;
		}

		protected void OnEnumPaymentEnumItemSelected (object sender, Gamma.Widgets.ItemSelectedEventArgs e)
		{
			enumDefaultDocumentType.Visible = labelDefaultDocumentType.Visible = (PaymentType)e.SelectedItem == PaymentType.cashless;
		}

		protected void OnReferencePreviousCounterpartyChangedByUser(object sender, EventArgs e)
		{
			if (DomainHelper.EqualDomainObjects(Entity.PreviousCounterparty, Entity))
				Entity.PreviousCounterparty = null;
		}
			
		protected void OnReferenceMainCounterpartyChangedByUser(object sender, EventArgs e)
		{
			if (DomainHelper.EqualDomainObjects(Entity.MainCounterparty, Entity))
				Entity.MainCounterparty = null;
		}

		protected void OnCheckSupplierToggled(object sender, EventArgs e)
		{
			labelDefaultExpense.Visible = referenceDefaultExpense.Visible = checkSupplier.Active;
		}

		protected void OnYentrySignPostFocusInEvent(object o, Gtk.FocusInEventArgs args)
		{
			if(yentrySignPost.Completion == null)
			{
				yentrySignPost.Completion = new Gtk.EntryCompletion();
				var list = Repository.CounterpartyRepository.GetUniqueSignatoryPosts(UoW);
				yentrySignPost.Completion.Model = ListStoreWorks.CreateFromEnumerable(list);
				yentrySignPost.Completion.TextColumn = 0;
				yentrySignPost.Completion.Complete();
			}
		}

		protected void OnYentrySignBaseOfFocusInEvent(object o, Gtk.FocusInEventArgs args)
		{
			if(yentrySignBaseOf.Completion == null)
			{
				yentrySignBaseOf.Completion = new Gtk.EntryCompletion();
				var list = Repository.CounterpartyRepository.GetUniqueSignatoryBaseOf(UoW);
				yentrySignBaseOf.Completion.Model = ListStoreWorks.CreateFromEnumerable(list);
				yentrySignBaseOf.Completion.TextColumn = 0;
				yentrySignBaseOf.Completion.Complete();
			}
		}

	}
}

