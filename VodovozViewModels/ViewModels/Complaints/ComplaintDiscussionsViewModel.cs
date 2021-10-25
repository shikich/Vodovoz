using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Autofac;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Journal;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Complaints;
using Vodovoz.EntityRepositories;
using Vodovoz.FilterViewModels.Organization;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Journals.JournalViewModels.Organization;

namespace Vodovoz.ViewModels.Complaints
{
	public class ComplaintDiscussionsViewModel : EntityWidgetViewModelBase<Complaint>
	{
		private readonly DialogViewModelBase _parrentViewModel;
		private readonly IFilePickerService _filePickerService;
		private readonly IEmployeeService _employeeService;
		private readonly IUserRepository _userRepository;
		private readonly INavigationManager _navigationManager;

		public ComplaintDiscussionsViewModel(
			Complaint entity, 
			DialogViewModelBase parrentViewModel,
			IUnitOfWork uow,
			IFilePickerService filePickerService,
			IEmployeeService employeeService,
			ICommonServices commonServices,
			IUserRepository userRepository,
			INavigationManager navigationManager
			) : base(entity, commonServices)
		{
			_filePickerService = filePickerService ?? throw new ArgumentNullException(nameof(filePickerService));
			_employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
			_parrentViewModel = parrentViewModel ?? throw new ArgumentNullException(nameof(parrentViewModel));
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));

			UoW = uow;
			CreateCommands();
			ConfigureEntityPropertyChanges();
			FillDiscussionsViewModels();
		}

		public bool CanEdit => PermissionResult.CanUpdate;

		private Dictionary<int, ComplaintDiscussionViewModel> viewModelsCache = new Dictionary<int, ComplaintDiscussionViewModel>();

		private void ConfigureEntityPropertyChanges()
		{
			Entity.ObservableComplaintDiscussions.ElementAdded += ObservableComplaintDiscussions_ElementAdded;
			Entity.ObservableComplaintDiscussions.ElementRemoved += ObservableComplaintDiscussions_ElementRemoved;
		}

		void ObservableComplaintDiscussions_ElementAdded(object aList, int[] aIdx)
		{
			FillDiscussionsViewModels();
		}

		void ObservableComplaintDiscussions_ElementRemoved(object aList, int[] aIdx, object aObject)
		{
			FillDiscussionsViewModels();
		}

		private void FillDiscussionsViewModels()
		{
			foreach(ComplaintDiscussion discussion in Entity.ObservableComplaintDiscussions) {
				var discussionViewModel = GetDiscussionViewModel(discussion);
				if(!ObservableComplaintDiscussionViewModels.Contains(discussionViewModel)) {
					ObservableComplaintDiscussionViewModels.Add(discussionViewModel);
				}
			}
		}

		private ComplaintDiscussionViewModel GetDiscussionViewModel(ComplaintDiscussion complaintDiscussion)
		{
			int subdivisionId = complaintDiscussion.Subdivision.Id;
			
			if(viewModelsCache.ContainsKey(subdivisionId))
			{
				return viewModelsCache[subdivisionId];
			}
			
			var viewModel =
				new ComplaintDiscussionViewModel(
					complaintDiscussion, _filePickerService, _employeeService, CommonServices, UoW, _userRepository);
			
			viewModelsCache.Add(subdivisionId, viewModel);
			return viewModel;
		}

		GenericObservableList<ComplaintDiscussionViewModel> observableComplaintDiscussionViewModels = new GenericObservableList<ComplaintDiscussionViewModel>();

		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<ComplaintDiscussionViewModel> ObservableComplaintDiscussionViewModels {
			get => observableComplaintDiscussionViewModels;
			set => SetField(ref observableComplaintDiscussionViewModels, value, () => ObservableComplaintDiscussionViewModels);
		}

		#region Commands

		private void CreateCommands()
		{
			CreateAttachSubdivisionCommand();
			CreateAttachSubdivisionByComplaintKindCommand();
		}

		#region AttachSubdivisionCommand

		public bool CanAttachSubdivision => CanEdit;

		public DelegateCommand AttachSubdivisionCommand { get; private set; }

		private void CreateAttachSubdivisionCommand()
		{
			AttachSubdivisionCommand = new DelegateCommand(
				() =>
				{
					var filterParams = new Action<SubdivisionFilterViewModel>[]
					{
						f => f.ExcludedSubdivisions = Entity.ObservableComplaintDiscussions.Select(x => x.Subdivision.Id).ToArray()
					};
					var page = _navigationManager.OpenViewModel<SubdivisionsJournalViewModel, Action<SubdivisionFilterViewModel>[]>(
						_parrentViewModel, filterParams, OpenPageOptions.AsSlave);
					page.ViewModel.SelectionMode = JournalSelectionMode.Single;
					page.ViewModel.OnEntitySelectedResult += (sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						if(selectedNode == null)
						{
							return;
						}
						Subdivision subdivision = UoW.GetById<Subdivision>(selectedNode.Id);
						Entity.AttachSubdivisionToDiscussions(subdivision);
					};
				},
				() => CanAttachSubdivision
			);
			AttachSubdivisionCommand.CanExecuteChangedWith(this, x => x.CanAttachSubdivision);
		}

		#endregion AttachSubdivisionCommand

		#region AttachSubdivisionByComplaintKindCommand

		public DelegateCommand AttachSubdivisionByComplaintKindCommand { get; private set; }

		private void CreateAttachSubdivisionByComplaintKindCommand()
		{
			AttachSubdivisionByComplaintKindCommand = new DelegateCommand(
				() =>
				{
					if(Entity.ComplaintKind == null)
					{
						CommonServices.InteractiveService.ShowMessage(ImportanceLevel.Warning, $"Не выбран вид рекламаций");
						return;
					}

					if(!Entity.ComplaintKind.Subdivisions.Any())
					{
						CommonServices.InteractiveService.ShowMessage(ImportanceLevel.Warning,
							$"У вида рекламации {Entity.ComplaintKind.Name} отсутствуют подключаемые отделы.");
						return;
					}

					string subdivisionString = string.Join(", ", Entity.ComplaintKind.Subdivisions.Select(s => s.Name));

					if(CommonServices.InteractiveService.Question(
						$"Будут подключены следующие отделы: { subdivisionString }.",
						"Подключить?")
					)
					{
						foreach(var subdivision in Entity.ComplaintKind.Subdivisions)
						{
							Entity.AttachSubdivisionToDiscussions(subdivision);
						}
					}
				},
				() => CanAttachSubdivision
			);
			AttachSubdivisionByComplaintKindCommand.CanExecuteChangedWith(this, x => x.CanAttachSubdivision);
		}

		#endregion AttachSubdivisionByComplaintKindCommand

		#endregion Commands
	}
}
