using System;
using System.Collections.Generic;
using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Control.EEVM;
using Vodovoz.Domain.Complaints;
using Vodovoz.EntityRepositories.Subdivisions;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ViewModels.Complaints
{
	public class GuiltyItemViewModel : EntityWidgetViewModelBase<ComplaintGuiltyItem>
	{
		private readonly DialogTabViewModelBase _parrentViewModel;

		public GuiltyItemViewModel(
			ComplaintGuiltyItem entity,
			ICommonServices commonServices,
			ISubdivisionRepository subdivisionRepository,
			IUnitOfWork uow,
			ILifetimeScope scope,
			DialogTabViewModelBase parrentViewModel
		) : base(entity, commonServices)
		{
			if(scope == null)
			{
				throw new ArgumentNullException(nameof(scope));
			}
			if(subdivisionRepository == null)
			{
				throw new ArgumentNullException(nameof(subdivisionRepository));
			}

			_parrentViewModel = parrentViewModel ?? throw new ArgumentNullException(nameof(parrentViewModel));

			UoW = uow ?? throw new ArgumentNullException(nameof(uow));
			ConfigureEntityPropertyChanges();
			CreateBindings(scope.Resolve<INavigationManager>(), scope);
			AllDepartments = subdivisionRepository.GetAllDepartmentsOrderedByName(UoW);
		}

		private void CreateBindings(INavigationManager navigationManager, ILifetimeScope scope)
		{
			var builder = new CommonEEVMBuilderFactory<ComplaintGuiltyItem>(_parrentViewModel, Entity, UoW, navigationManager, scope);

			EmployeeViewModel = builder.ForProperty(e => e.Employee)
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel>()
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
		}

		public event EventHandler OnGuiltyItemReady;

		private IList<Subdivision> allDepartments;
		public IList<Subdivision> AllDepartments {
			get => allDepartments;
			private set => SetField(ref allDepartments, value);
		}

		public bool CanChooseEmployee => Entity.GuiltyType == ComplaintGuiltyTypes.Employee;

		public bool CanChooseSubdivision => Entity.GuiltyType == ComplaintGuiltyTypes.Subdivision;

		public IEntityEntryViewModel EmployeeViewModel { get; private set; }

		void ConfigureEntityPropertyChanges()
		{
			SetPropertyChangeRelation(
				e => e.GuiltyType,
				() => CanChooseEmployee,
				() => CanChooseSubdivision
			);

			OnEntityPropertyChanged(
				() => OnGuiltyItemReady?.Invoke(this, EventArgs.Empty),
				e => e.GuiltyType,
				e => e.Employee,
				e => e.Subdivision
			);
		}
	}
}
