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
			
			buttonSave.Clicked += (sender, args) => ViewModel.SaveAndClose();
			buttonCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);
		}
	}
}
