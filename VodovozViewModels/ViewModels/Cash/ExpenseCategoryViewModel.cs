using System;
using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Cash;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Journals.JournalFactories;
using Vodovoz.ViewModels.Journals.JournalViewModels.Cash;

namespace Vodovoz.ViewModels.ViewModels.Cash
{
	public class ExpenseCategoryViewModel : EntityTabViewModelBase<ExpenseCategory>
	{
		public ExpenseCategoryViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ISubdivisionJournalFactory subdivisionJournalFactory,
			ILifetimeScope scope,
			INavigationManager navigationManager = null
		) : base(uowBuilder, unitOfWorkFactory, commonServices, navigationManager, scope)
		{
			if(subdivisionJournalFactory == null)
			{
				throw new ArgumentNullException(nameof(subdivisionJournalFactory));
			}
			
			ExpenseCategoryAutocompleteSelectorFactory = 
				new EntityAutocompleteSelectorFactory<ExpenseCategoryJournalViewModel>(
					typeof(IncomeCategory),
					() => Scope.Resolve<ExpenseCategoryJournalViewModel>());

			SubdivisionAutocompleteSelectorFactory = subdivisionJournalFactory.CreateDefaultSubdivisionAutocompleteSelectorFactory(Scope);
			
			if(uowBuilder.IsNewEntity)
				TabName = "Создание новой категории расхода";
			else
				TabName = $"{Entity.Title}";
			
		}
		
		public IEntityAutocompleteSelectorFactory SubdivisionAutocompleteSelectorFactory { get; }

		public bool IsArchive
		{
			get { return Entity.IsArchive; }
			set
			{
				Entity.SetIsArchiveRecursively(value);
			}
		}

		public readonly IEntityAutocompleteSelectorFactory ExpenseCategoryAutocompleteSelectorFactory;

		#region Permissions

		public bool CanCreate => PermissionResult.CanCreate;
		public bool CanUpdate => PermissionResult.CanUpdate;

		#endregion
	}
}
