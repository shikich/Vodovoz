using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gamma.Widgets;
using QS.Banks.Domain;
using QS.Dialog;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.DB;
using QS.Project.Services;
using QS.Views.GtkUI;
using QS.Widgets.GtkUI;
using QSOrmProject;
using Vodovoz.Dialogs.Employees;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Logistic;
using Vodovoz.ViewModels.ViewModels.Employees;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.ViewModels.Orders;
using Vodovoz.Domain.Orders;

namespace Vodovoz.Views.Orders
{
	public partial class DiscountReasonView : TabViewBase<DiscountReasonViewModel>, IEntityDialog
	{
		public DiscountReasonView(DiscountReasonViewModel viewModel) : base(viewModel)
		{
			this.Build();
			Configure();
		}

		public IUnitOfWork UoW => ViewModel.UoW;
		public object EntityObject => ViewModel.UoWGeneric.RootObject;

		private void Configure()
		{
			entryName.Binding.AddBinding(ViewModel.Entity, dr => dr.Name, w => w.Text).InitializeFromSource();
			checkIsArchive.Binding.AddBinding(ViewModel.Entity, dr => dr.IsArchive, w => w.Active).InitializeFromSource();
			yenumTargetType.ItemsEnum = typeof(DiscountTargetType);
			yenumTargetType.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountTargetType, w => w.SelectedItem).InitializeFromSource();
			yenumPermissionType.ItemsEnum = typeof(DiscountPermissionType);
			yenumPermissionType.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountPermissionType, w => w.SelectedItem).InitializeFromSource();
			yentryDiscountMinValue.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountMinValue, w => w.Text).InitializeFromSource();
			yentryDiscountMaxValue.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountMaxValue, w => w.Text).InitializeFromSource();
			yenumValueType.ItemsEnum = typeof(DiscountValueType);
			yenumValueType.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountValueType, w => w.SelectedItem).InitializeFromSource();
			yenumUsage.ItemsEnum = typeof(DiscountUsage);
			yenumUsage.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountUsage, w => w.SelectedItem).InitializeFromSource();
			yenumCounterpartyType.ItemsEnum = typeof(DiscountCounterpartyType);
			yenumCounterpartyType.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountCounterpartyType, w => w.SelectedItem).InitializeFromSource();
			yentryForCounterpartyOrDP.Binding.AddBinding(ViewModel.Entity, dr => dr.CounterpartyOrDPName, w => w.Text).InitializeFromSource();
			ViewModel.DiscountsViewModel.DiscountsList = new GenericObservableList<Discount>();
			discountsview1.ViewModel = ViewModel.DiscountsViewModel;
			discountsview1.ViewModel.DiscountsList = new GenericObservableList<Discount>(ViewModel.Entity.Discounts);

			comboSkillLevel.ItemsList = ViewModel.Entity.GetSkillLevels();
			comboSkillLevel.Binding
				.AddBinding(
					ViewModel.Entity,
					e => e.SkillLevel,
					w => w.ActiveText,
					new Gamma.Binding.Converters.NumbersToStringConverter()
				).InitializeFromSource();
			comboSkillLevel.SelectedItem = ViewModel.Entity.SkillLevel;

			ycheckSelfdelivery.Binding
				.AddBinding(ViewModel.Entity, e => e.ForSelfdelivery, w => w.Active)
				.InitializeFromSource();
			ycheckForPromotionalSet.Binding
				.AddBinding(ViewModel.Entity, e => e.ForPromotionalSet, w => w.Active)
				.InitializeFromSource();
			ycheckIncludeReclamation.Binding
				.AddBinding(ViewModel.Entity, e => e.IncludeReclamation, w => w.Active)
				.InitializeFromSource();

			yenumWithOtherDiscounts.ItemsEnum = typeof(DiscountWithOtherDiscounts);
			yenumWithOtherDiscounts.Binding.AddBinding(ViewModel.Entity, dr => dr.DiscountWithOtherDiscounts, w => w.SelectedItem).InitializeFromSource();


			buttonSave.Clicked += (sender, args) => ViewModel.SaveAndClose();
			buttonCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);
		}

	}
}
