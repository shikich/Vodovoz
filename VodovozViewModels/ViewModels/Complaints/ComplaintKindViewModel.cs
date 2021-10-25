using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Complaints;
using Vodovoz.Journals.JournalViewModels.Organization;

namespace Vodovoz.ViewModels.Complaints
{
	public class ComplaintKindViewModel : EntityTabViewModelBase<ComplaintKind>
	{
		private DelegateCommand<Subdivision> _removeSubdivisionCommand;
		private DelegateCommand _attachSubdivisionCommand;
		private readonly Action _updateJournalAction;
		private readonly IList<Subdivision> _subdivisionsOnStart;

		public ComplaintKindViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			Action updateJournalAction,
			INavigationManager navigationManager,
			ILifetimeScope scope) : base(uowBuilder, unitOfWorkFactory, commonServices, navigationManager, scope)
		{
			_updateJournalAction = updateJournalAction ?? throw new ArgumentNullException(nameof(updateJournalAction));

			ComplaintObjects = UoW.Session.QueryOver<ComplaintObject>().List();
			_subdivisionsOnStart = new List<Subdivision>(Entity.Subdivisions);

			TabName = "Виды рекламаций";
		}

		protected override void AfterSave()
		{
			var isEqualSubdivisionLists = new HashSet<Subdivision>(_subdivisionsOnStart).SetEquals(Entity.Subdivisions);

			if(!isEqualSubdivisionLists)
			{
				_updateJournalAction.Invoke();
			}

			base.AfterSave();
		}

		public IList<ComplaintObject> ComplaintObjects { get; }

		#region Commands

		public DelegateCommand AttachSubdivisionCommand => _attachSubdivisionCommand ?? (_attachSubdivisionCommand = new DelegateCommand(() =>
				{
					/*var subdivisionFilter = new SubdivisionFilterViewModel();
					var subdivisionJournalViewModel = new SubdivisionsJournalViewModel(
						subdivisionFilter,
						_unitOfWorkFactory,
						_commonServices,
						_salesPlanJournalFactory,
						_nomenclatureSelectorFactory
					);
					subdivisionJournalViewModel.SelectionMode = JournalSelectionMode.Single;
					subdivisionJournalViewModel.OnEntitySelectedResult += (sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						if(selectedNode == null)
						{
							return;
						}
						Entity.AddSubdivision(UoW.GetById<Subdivision>(selectedNode.Id));
					};
					TabParent.AddSlaveTab(this, subdivisionJournalViewModel);*/
					var page = NavigationManager.OpenViewModel<SubdivisionsJournalViewModel>(this, OpenPageOptions.AsSlave);
					page.ViewModel.OnEntitySelectedResult += (sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						if(selectedNode == null)
						{
							return;
						}
						Entity.AddSubdivision(UoW.GetById<Subdivision>(selectedNode.Id));
					};
				},
				() => true
			));


		public DelegateCommand<Subdivision> RemoveSubdivisionCommand => _removeSubdivisionCommand ?? (_removeSubdivisionCommand =
			new DelegateCommand<Subdivision>((subdivision) =>
				{
					Entity.RemoveSubdivision(subdivision);
				},
				(subdivision) => true
			));

		#endregion Commands

	}
}
