﻿using System.Collections.Generic;
using QS.Views.GtkUI;
using Vodovoz.Domain.Complaints;
using Vodovoz.ViewModels.Complaints;

namespace Vodovoz.Views.Complaints
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class GuiltyItemView : WidgetViewBase<GuiltyItemViewModel>
	{
		public GuiltyItemView()
		{
			this.Build();
		}

		public GuiltyItemView(GuiltyItemViewModel viewModel) : base(viewModel)
		{
			this.Build();
		}

		protected override void ConfigureWidget()
		{
			yEnumGuiltyType.ItemsEnum = typeof(ComplaintGuiltyTypes);
			yEnumGuiltyType.Binding.AddBinding(ViewModel.Entity, s => s.GuiltyType, w => w.SelectedItemOrNull).InitializeFromSource();
			employeeEntry.ViewModel.IsEditable = false;
			//entVmEmployee.SetEntityAutocompleteSelectorFactory(ViewModel.EmployeeSelectorFactory);
			//entVmEmployee.Binding.AddBinding(ViewModel.Entity, e => e.Employee, w => w.Subject).InitializeFromSource();
			//entVmEmployee.Binding.AddBinding(ViewModel, vm => vm.CanChooseEmployee, w => w.Visible).InitializeFromSource();
			yCmbSubdivision.SetRenderTextFunc<Subdivision>(
				s => {
					List<string> strLst = new List<string>();
					if(!string.IsNullOrWhiteSpace(s.ShortName))
						strLst.Add(string.Format("({0}) ", s.ShortName));
					if(!string.IsNullOrWhiteSpace(s.Name))
						strLst.Add(s.Name);
					return string.Concat(strLst);
				}
			);
			yCmbSubdivision.ItemsList = ViewModel.AllDepartments;
			yCmbSubdivision.Binding.AddBinding(ViewModel.Entity, s => s.Subdivision, w => w.SelectedItem).InitializeFromSource();
			yCmbSubdivision.Binding.AddBinding(ViewModel, vm => vm.CanChooseSubdivision, w => w.Visible).InitializeFromSource();
			yCmbSubdivision.SetSizeRequest(250, 30);

			this.Shown += (s, ea) => {
				employeeEntry.Visible = ViewModel.CanChooseEmployee;
				yCmbSubdivision.Visible = ViewModel.CanChooseSubdivision;
			};
		}
	}
}