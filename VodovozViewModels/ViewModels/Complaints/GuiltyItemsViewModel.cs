﻿using System;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Complaints;
using Vodovoz.EntityRepositories.Subdivisions;

namespace Vodovoz.ViewModels.Complaints
{
	public class GuiltyItemsViewModel : EntityWidgetViewModelBase<Complaint>
	{
		readonly ISubdivisionRepository subdivisionRepository;
		readonly ICommonServices commonServices;
		readonly IEntityAutocompleteSelectorFactory employeeSelectorFactory;

		public GuiltyItemsViewModel(
			Complaint entity,
			IUnitOfWork uow,
			ICommonServices commonServices,
			ISubdivisionRepository subdivisionRepository,
			IEntityAutocompleteSelectorFactory employeeSelectorFactory
		) : base(entity, commonServices)
		{
			this.employeeSelectorFactory = employeeSelectorFactory ?? throw new ArgumentNullException(nameof(employeeSelectorFactory));
			this.subdivisionRepository = subdivisionRepository ?? throw new ArgumentNullException(nameof(subdivisionRepository));
			this.commonServices = commonServices;
			UoW = uow ?? throw new ArgumentNullException(nameof(uow));
			CreateCommands();
			UpdateAcessibility();
		}

		GuiltyItemViewModel currentGuiltyVM;
		public virtual GuiltyItemViewModel CurrentGuiltyVM {
			get => currentGuiltyVM;
			set => SetField(ref currentGuiltyVM, value, () => CurrentGuiltyVM);
		}

		private bool canRemoveGuilty;
		public virtual bool CanRemoveGuilty {
			get => canRemoveGuilty;
			set => SetField(ref canRemoveGuilty, value, () => CanRemoveGuilty);
		}


		bool canEditGuilty;
		public bool CanEditGuilty {
			get => canEditGuilty;
			set => SetField(ref canEditGuilty, value, () => CanEditGuilty);
		}

        public bool CanAddGuilty => ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_add_guilty_in_complaints")
			&& CurrentGuiltyVM == null;

		void UpdateAcessibility()
		{
			CanEditGuilty = !CanAddGuilty;
		}

		void CreateItem()
		{
			CurrentGuiltyVM = new GuiltyItemViewModel(
				new ComplaintGuiltyItem(),
				commonServices,
				subdivisionRepository,
				employeeSelectorFactory,
				UoW
			);
			UpdateAcessibility();
		}

		void ClearItem()
		{
			CurrentGuiltyVM = null;
			UpdateAcessibility();
		}

		#region Commands

		void CreateCommands()
		{
			CreateAddGuiltyCommand();
			CreateRemoveGuiltyCommand();
			CreateSaveGuiltyCommand();
			CreateCancelCommand();
		}

		#region AddGuiltyCommand

		public DelegateCommand AddGuiltyCommand { get; private set; }
		private void CreateAddGuiltyCommand()
		{
			AddGuiltyCommand = new DelegateCommand(
				CreateItem,
				() => CanAddGuilty
			);
		}

		#endregion AddGuiltyCommand

		#region SaveGuiltyCommand

		public DelegateCommand SaveGuiltyCommand { get; private set; }

		private void CreateSaveGuiltyCommand()
		{
			SaveGuiltyCommand = new DelegateCommand(
				() => {
					if(CurrentGuiltyVM.Entity.GuiltyType != ComplaintGuiltyTypes.Employee)
						CurrentGuiltyVM.Entity.Employee = null;
					if(CurrentGuiltyVM.Entity.GuiltyType != ComplaintGuiltyTypes.Subdivision)
						CurrentGuiltyVM.Entity.Subdivision = null;
					CurrentGuiltyVM.Entity.Complaint = Entity;
					Entity.ObservableGuilties.Add(CurrentGuiltyVM.Entity);
					ClearItem();
				},
				() => CanEditGuilty
			);
		}

		#endregion SaveGuiltyCommand

		#region CancelCommand

		public DelegateCommand CancelCommand { get; private set; }

		private void CreateCancelCommand()
		{
			CancelCommand = new DelegateCommand(
				ClearItem,
				() => CanEditGuilty
			);
		}

		#endregion CancelCommand

		#region RemoveGuiltyCommand

		public DelegateCommand<ComplaintGuiltyItem> RemoveGuiltyCommand { get; private set; }
		private void CreateRemoveGuiltyCommand()
		{
			RemoveGuiltyCommand = new DelegateCommand<ComplaintGuiltyItem>(
				g => Entity.ObservableGuilties.Remove(g),
				g => CanRemoveGuilty
			);
			RemoveGuiltyCommand.CanExecuteChangedWith(this, x => x.CanRemoveGuilty);
		}

		#endregion RemoveGuiltyCommand

		#endregion Commands
	}
}
