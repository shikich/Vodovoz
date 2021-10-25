using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NLog;
using QS.Commands;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Complaints;
using Vodovoz.Domain.Employees;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.FilterViewModels.Employees;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Journals.JournalViewModels.Employees;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Employees;

namespace Vodovoz.ViewModels.Complaints
{
	public class ComplaintViewModel : EntityTabViewModelBase<Complaint>
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly IFilePickerService _filePickerService;
		private IList<ComplaintObject> _complaintObjectSource;
		private ComplaintObject _complaintObject;
		private readonly IList<ComplaintKind> _complaintKinds;
		private DelegateCommand _changeDeliveryPointCommand;

		public IEntityAutocompleteSelectorFactory CounterpartySelectorFactory { get; }
		public IEmployeeService EmployeeService { get; }
		public INomenclatureRepository NomenclatureRepository { get; }
		public IUserRepository UserRepository { get; }
		
		public ComplaintViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory uowFactory,
			ICommonServices commonServices,
			IEmployeeService employeeService,
			IEntityAutocompleteSelectorFactory counterpartySelectorFactory,
			IFilePickerService filePickerService,
			INomenclatureRepository nomenclatureRepository,
			IUserRepository userRepository,
			INavigationManager navigationManager,
			ILifetimeScope scope) : base(uowBuilder, uowFactory, commonServices, navigationManager, scope)
		{
			_filePickerService = filePickerService ?? throw new ArgumentNullException(nameof(filePickerService));
			CounterpartySelectorFactory = counterpartySelectorFactory ?? throw new ArgumentNullException(nameof(counterpartySelectorFactory));
			EmployeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
			NomenclatureRepository = nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
			UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

			Entity.ObservableComplaintDiscussions.ElementChanged += ObservableComplaintDiscussions_ElementChanged;
			Entity.ObservableComplaintDiscussions.ListContentChanged += ObservableComplaintDiscussions_ListContentChanged;
			Entity.ObservableFines.ListContentChanged += ObservableFines_ListContentChanged;

			if(uowBuilder.IsNewEntity) {
				AbortOpening("Невозможно создать новую рекламацию из текущего диалога, необходимо использовать диалоги создания");
			}

			if(CurrentEmployee == null) {
				AbortOpening("Невозможно открыть рекламацию так как к вашему пользователю не привязан сотрудник");
			}

			ConfigureEntityChangingRelations();
			CreateOrderSelectorFactory();
			CreateCommands();

			_complaintKinds = complaintKindSource = UoW.GetAll<ComplaintKind>().Where(k => !k.IsArchive).ToList();

			ComplaintObject = Entity.ComplaintKind?.ComplaintObject;

			TabName = $"Рекламация №{Entity.Id} от {Entity.CreationDate.ToShortDateString()}";
		}

		protected void ConfigureEntityChangingRelations()
		{
			SetPropertyChangeRelation(e => e.ComplaintType,
				() => IsInnerComplaint,
				() => IsClientComplaint
			);

			SetPropertyChangeRelation(e => e.Status,
				() => Status
			);

			SetPropertyChangeRelation(
				e => e.ChangedBy,
				() => ChangedByAndDate
			);

			SetPropertyChangeRelation(
				e => e.ChangedDate,
				() => ChangedByAndDate
			);

			SetPropertyChangeRelation(
				e => e.CreatedBy,
				() => CreatedByAndDate
			);

			SetPropertyChangeRelation(
				e => e.CreationDate,
				() => CreatedByAndDate
			);

			SetPropertyChangeRelation(
				e => e.Counterparty,
				() => CanSelectDeliveryPoint
			);
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

		void ObservableComplaintDiscussions_ElementChanged(object aList, int[] aIdx)
		{
			OnDiscussionsChanged();
		}

		void ObservableComplaintDiscussions_ListContentChanged(object sender, EventArgs e)
		{
			OnDiscussionsChanged();
		}

		private void OnDiscussionsChanged()
		{
			OnPropertyChanged(() => SubdivisionsInWork);
			Entity.UpdateComplaintStatus();
		}

		void ObservableFines_ListContentChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(() => FineItems);
		}

		private Employee currentEmployee;
		public Employee CurrentEmployee {
			get {
				if(currentEmployee == null) {
					currentEmployee = EmployeeService.GetEmployeeForUser(UoW, CommonServices.UserService.CurrentUserId);
				}
				return currentEmployee;
			}
		}

		public virtual ComplaintStatuses Status {
			get => Entity.Status;
			set {
				var msg = Entity.SetStatus(value);
				if(!msg.Any())
					Entity.ActualCompletionDate = value == ComplaintStatuses.Closed ? (DateTime?)DateTime.Now : null;
				else
					ShowWarningMessage(string.Join<string>("\n", msg), "Не удалось закрыть");
				OnPropertyChanged(() => Status);
			}
		}

		private ComplaintDiscussionsViewModel discussionsViewModel;
		public ComplaintDiscussionsViewModel DiscussionsViewModel {
			get {
				if(discussionsViewModel == null) {
					discussionsViewModel = new ComplaintDiscussionsViewModel(
						Entity,
						this,
						UoW,
						_filePickerService,
						EmployeeService,
						CommonServices,
						UserRepository,
						NavigationManager
					);
				}
				return discussionsViewModel;
			}
		}

		private GuiltyItemsViewModel guiltyItemsViewModel;
		public GuiltyItemsViewModel GuiltyItemsViewModel
		{
			get
			{
				if(guiltyItemsViewModel == null)
				{
					guiltyItemsViewModel = new GuiltyItemsViewModel(Entity, UoW, CommonServices, Scope, this);
				}

				return guiltyItemsViewModel;
			}
		}


		private ComplaintFilesViewModel filesViewModel;
		public ComplaintFilesViewModel FilesViewModel
		{
			get
			{
				if(filesViewModel == null)
				{
					filesViewModel = new ComplaintFilesViewModel(Entity, UoW, _filePickerService, CommonServices, UserRepository);
				}
				return filesViewModel;
			}
		}

		public string SubdivisionsInWork {
			get {
				string inWork = string.Join(", ", Entity.ComplaintDiscussions
					.Where(x => x.Status == ComplaintStatuses.InProcess)
					.Where(x => !string.IsNullOrWhiteSpace(x.Subdivision?.ShortName))
					.Select(x => x.Subdivision.ShortName));
				string okk = (!Entity.ComplaintDiscussions.Any(x => x.Status == ComplaintStatuses.InProcess) && Status != ComplaintStatuses.Closed) ? "OKK" : null;
				string result;
				if(!string.IsNullOrWhiteSpace(inWork) && !string.IsNullOrWhiteSpace(okk)) {
					result = string.Join(", ", inWork, okk);
				} else if(!string.IsNullOrWhiteSpace(inWork)) {
					result = inWork;
				} else if(!string.IsNullOrWhiteSpace(okk)) {
					result = okk;
				} else {
					return string.Empty;
				}
				return $"В работе у отд. {result}";
			}
		}

		public string ChangedByAndDate => string.Format("Изм: {0} {1}", Entity.ChangedBy?.ShortName, Entity.ChangedDate.ToString("g"));
		public string CreatedByAndDate => string.Format("Созд: {0} {1}", Entity.CreatedBy?.ShortName, Entity.CreationDate.ToString("g"));

		private List<ComplaintSource> complaintSources;
		public IEnumerable<ComplaintSource> ComplaintSources {
			get {
				if(complaintSources == null) {
					complaintSources = UoW.GetAll<ComplaintSource>().ToList();
				}
				return complaintSources;
			}
		}

		private List<ComplaintResult> complaintResults;
		public IEnumerable<ComplaintResult> ComplaintResults {
			get {
				if(complaintResults == null) {
					complaintResults = UoW.GetAll<ComplaintResult>().ToList();
				}
				return complaintResults;
			}
		}

		IList<ComplaintKind> complaintKindSource;

		public IList<ComplaintKind> ComplaintKindSource {
			get {
				if(Entity.ComplaintKind != null && Entity.ComplaintKind.IsArchive)
					complaintKindSource.Add(UoW.GetById<ComplaintKind>(Entity.ComplaintKind.Id));

				return complaintKindSource;
			}
			set
			{
				SetField(ref complaintKindSource, value);
			}
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

		public IList<FineItem> FineItems => Entity.Fines.SelectMany(x => x.Items).OrderByDescending(x => x.Id).ToList();

		public bool IsInnerComplaint => Entity.ComplaintType == ComplaintType.Inner;

		public bool IsClientComplaint => Entity.ComplaintType == ComplaintType.Client;

		[PropertyChangedAlso(nameof(CanAddFine), nameof(CanAttachFine))]
		public bool CanEdit => PermissionResult.CanUpdate;

		public bool CanAddGuilty => CommonServices.CurrentPermissionService.ValidatePresetPermission("can_add_guilty_in_complaints");
		public bool CanClose => CommonServices.CurrentPermissionService.ValidatePresetPermission("can_close_complaints");

		public bool CanSelectDeliveryPoint => Entity.Counterparty != null;

		public bool CanAddFine => CanEdit;
		public bool CanAttachFine => CanEdit;

		#region Commands

		private void CreateCommands()
		{
			CreateAttachFineCommand();
			CreateAddFineCommand();
		}

		#region AttachFineCommand

		public DelegateCommand AttachFineCommand { get; private set; }

		private void CreateAttachFineCommand()
		{
			AttachFineCommand = new DelegateCommand(
				() =>
				{
					/*var fineFilter = new FineFilterViewModel();
					fineFilter.ExcludedIds = Entity.Fines.Select(x => x.Id).ToArray();
					var fineJournalViewModel = new FinesJournalViewModel(
						fineFilter,
						EmployeeService,
						_employeeSelectorFactory,
						QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
						CommonServices,
						NavigationManager
					);*/
					var filterParams = new Action<FineFilterViewModel>[]
					{
						x => x.ExcludedIds = Entity.Fines.Select(fine => fine.Id).ToArray()
					};
					var page = NavigationManager.OpenViewModel<FinesJournalViewModel, Action<FineFilterViewModel>[]>(
						this, filterParams, OpenPageOptions.AsSlave);
					page.ViewModel.SelectionMode = JournalSelectionMode.Single;
					page.ViewModel.OnEntitySelectedResult += (sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						if(selectedNode == null)
						{
							return;
						}
						Entity.AddFine(UoW.GetById<Fine>(selectedNode.Id));
					};
					//TabParent.AddSlaveTab(this, fineJournalViewModel);
				},
				() => CanAttachFine
			);
			AttachFineCommand.CanExecuteChangedWith(this, x => CanAttachFine);
		}

		#endregion AttachFineCommand

		#region AddFineCommand

		public DelegateCommand<DialogViewModelBase> AddFineCommand { get; private set; }

		private void CreateAddFineCommand()
		{
			AddFineCommand = new DelegateCommand<DialogViewModelBase>(
				journal => {
					/*FineViewModel fineViewModel = new FineViewModel(
						EntityUoWBuilder.ForCreate(),
						QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
						_undeliveryViewOpener,
						EmployeeService,
						_employeeSelectorFactory,
						CommonServices
					);
					fineViewModel.FineReasonString = Entity.GetFineReason();
					fineViewModel.EntitySaved += (sender, e) => {
						Entity.AddFine(e.Entity as Fine);
					};
					t.TabParent.AddSlaveTab(t, fineViewModel);*/
					var page = NavigationManager.OpenViewModel<FineViewModel, IEntityUoWBuilder>(
						journal, EntityUoWBuilder.ForCreate(), OpenPageOptions.AsSlave);
					page.ViewModel.FineReasonString = Entity.GetFineReason();
					page.ViewModel.EntitySaved += (sender, e) =>
					{
						Entity.AddFine(e.Entity as Fine);
					};
				},
				t => CanAddFine
			);
			AddFineCommand.CanExecuteChangedWith(this, x => CanAddFine);
		}

		#endregion AddFineCommand

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

		#endregion Commands
		
		public IEntityAutocompleteSelectorFactory OrderSelectorFactory { get; private set; }
	}
}
