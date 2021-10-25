using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Complaints;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories;
using Vodovoz.Infrastructure.Services;
using Vodovoz.TempAdapters;

namespace Vodovoz.ViewModels.Complaints
{
	public class CreateComplaintViewModel : EntityTabViewModelBase<Complaint>
	{
        private readonly IFilePickerService _filePickerService;
		private IList<ComplaintObject> _complaintObjectSource;
        private ComplaintObject _complaintObject;
        private readonly IList<ComplaintKind> _complaintKinds;
        private DelegateCommand _changeDeliveryPointCommand;

		public ICounterpartyJournalFactory CounterpartyJournalFactory { get; }
		public IEmployeeService EmployeeService { get; }
		public IUserRepository UserRepository { get; }
		public bool UserHasOnlyAccessToWarehouseAndComplaints { get; }

		public CreateComplaintViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			IEmployeeService employeeService,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			ICommonServices commonServices,
			IUserRepository userRepository,
            IFilePickerService filePickerService,
			ILifetimeScope scope,
			string phone = null) : base(uowBuilder, unitOfWorkFactory, commonServices, null, scope)
		{
            _filePickerService = filePickerService ?? throw new ArgumentNullException(nameof(filePickerService));
			EmployeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
			UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			CounterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));

			Entity.ComplaintType = ComplaintType.Client;
			Entity.SetStatus(ComplaintStatuses.Checking);
			ConfigureEntityPropertyChanges();
			CreateOrderSelectorFactory();
			Entity.Phone = phone;

			_complaintKinds = complaintKindSource = UoW.GetAll<ComplaintKind>().Where(k => !k.IsArchive).ToList();

			UserHasOnlyAccessToWarehouseAndComplaints =
				ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("user_have_access_only_to_warehouse_and_complaints")
			    && !ServicesConfig.CommonServices.UserService.GetCurrentUser(UoW).IsAdmin;

			TabName = "Новая клиентская рекламация";
		}

		public CreateComplaintViewModel(Counterparty client,
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			IEmployeeService employeeService,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			ICommonServices commonServices,
			IUserRepository userRepository,
            IFilePickerService filePickerService,
			ILifetimeScope scope,
			string phone = null) : this(uowBuilder, unitOfWorkFactory, employeeService, counterpartyJournalFactory, commonServices,
			userRepository, filePickerService, scope, phone)
		{
			var currentClient = UoW.GetById<Counterparty>(client.Id);
			Entity.Counterparty = currentClient;
			Entity.Phone = phone;
		}

		public CreateComplaintViewModel(
			Order order,
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			IEmployeeService employeeService,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			ICommonServices commonServices,
			IUserRepository userRepository,
            IFilePickerService filePickerService,
			ILifetimeScope scope,
			string phone = null) : this(uowBuilder, unitOfWorkFactory, employeeService, counterpartyJournalFactory, commonServices,
			userRepository, filePickerService, scope, phone)
		{
			var currentOrder = UoW.GetById<Order>(order.Id);
			Entity.Order = currentOrder;
			Entity.Counterparty = currentOrder.Client;
			Entity.Phone = phone;
		}

		private Employee currentEmployee;
		public Employee CurrentEmployee {
			get {
				if(currentEmployee == null) {
					currentEmployee = EmployeeService.GetEmployeeForUser(UoW, UserService.CurrentUserId);
				}
				return currentEmployee;
			}
		}

        private ComplaintFilesViewModel filesViewModel;
        public ComplaintFilesViewModel FilesViewModel
        {
            get
            {
                if (filesViewModel == null)
                {
                    filesViewModel = new ComplaintFilesViewModel(Entity, UoW, _filePickerService, CommonServices, UserRepository);
                }
                return filesViewModel;
            }
        }

        //так как диалог только для создания рекламации
        public bool CanEdit => PermissionResult.CanCreate;

		public bool CanSelectDeliveryPoint => Entity.Counterparty != null;

		private List<ComplaintSource> complaintSources;

		public IEnumerable<ComplaintSource> ComplaintSources {
			get {
				if(complaintSources == null) {
					complaintSources = UoW.GetAll<ComplaintSource>().ToList();
				}
				return complaintSources;
			}
		}

		IList<ComplaintKind> complaintKindSource;
		public IList<ComplaintKind> ComplaintKindSource {
			get => complaintKindSource;
			set => SetField(ref complaintKindSource, value);
		}

		public virtual ComplaintObject ComplaintObject
		{
			get => _complaintObject;
			set
			{
				if(SetField(ref _complaintObject, value))
				{
					ComplaintKindSource = value == null ? _complaintKinds : _complaintKinds.Where(x => x.ComplaintObject == value).ToList();
				}
			}
		}

		public IEnumerable<ComplaintObject> ComplaintObjectSource =>
			_complaintObjectSource ?? (_complaintObjectSource = UoW.GetAll<ComplaintObject>().Where(x => !x.IsArchive).ToList());

		private GuiltyItemsViewModel guiltyItemsViewModel;
		public GuiltyItemsViewModel GuiltyItemsViewModel
		{
			get
			{
				if(guiltyItemsViewModel == null)
				{
					var parameters = new Parameter[]
					{
						new TypedParameter(typeof(Complaint), Entity),
						new TypedParameter(typeof(IUnitOfWork), UoW),
						new TypedParameter(typeof(DialogTabViewModelBase), this)
					};
					
					guiltyItemsViewModel = Scope.Resolve<GuiltyItemsViewModel>(parameters);
				}

				return guiltyItemsViewModel;
			}
		}

		protected override void BeforeValidation()
		{
			if(UoW.IsNew) {
				Entity.CreatedBy = CurrentEmployee;
				Entity.CreationDate = DateTime.Now;
				Entity.PlannedCompletionDate = DateTime.Today;
			}
			Entity.ChangedBy = CurrentEmployee;
			Entity.ChangedDate = DateTime.Now;

			base.BeforeValidation();
		}

		private void CreateOrderSelectorFactory()
		{
			if(Entity.Counterparty != null)
			{
				OrderSelectorFactory =
					Scope.Resolve<IOrderSelectorFactory>().CreateOrderAutocompleteSelectorFactory(
						Scope, x => x.RestrictCounterparty = Entity.Counterparty);
			}
			else
			{
				OrderSelectorFactory =
					Scope.Resolve<IOrderSelectorFactory>().CreateOrderAutocompleteSelectorFactory(Scope);
			}
		}

		void ConfigureEntityPropertyChanges()
		{
			SetPropertyChangeRelation(
				e => e.Counterparty,
				() => CanSelectDeliveryPoint
			);
		}

        public void CheckAndSave()
        {
            if (!HasСounterpartyDuplicateToday() ||
                CommonServices.InteractiveService.Question("Рекламация с данным контрагентом уже создавалась сегодня, создать ещё одну?"))
            {
                SaveAndClose();
            }
        }

        private bool HasСounterpartyDuplicateToday()
        {
	        if(Entity.Counterparty == null) {
		        return false;
	        }
	        return UoW.Session.QueryOver<Complaint>()
		        .Where(i => i.Counterparty.Id == Entity.Counterparty.Id)
		        .And(i => i.CreationDate >= DateTime.Now.AddDays(-1))
		        .RowCount() > 0;
        }

        #region ChangeDeliveryPointCommand

        public DelegateCommand ChangeDeliveryPointCommand => _changeDeliveryPointCommand ?? (_changeDeliveryPointCommand =
	        new DelegateCommand(() =>
		        {
			        if(Entity.Order?.DeliveryPoint != null)
			        {
				        Entity.DeliveryPoint = Entity.Order.DeliveryPoint;
			        }
		        },
		        () => true
	        ));

        #endregion ChangeDeliveryPointCommand

		public IEntityAutocompleteSelectorFactory OrderSelectorFactory { get; private set; }
	}
}
