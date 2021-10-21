using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.GtkWidgets;
using Gamma.Widgets;
using Gtk;
using NLog;
using QS.DomainModel.UoW;
using QSWidgetLib;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories;
using Vodovoz.Infrastructure.Converters;
using Vodovoz.Parameters;
using Vodovoz.ViewModels.ViewModels.Contacts;

namespace Vodovoz.Views.Orders
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DiscountsView : Gtk.Bin
	{
		private DiscountsViewModel viewModel;

		private IList<VBox> VBoxList;

		public DiscountsViewModel ViewModel
		{
			get { return viewModel; }
			set
			{
				viewModel = value;
				ConfigureDlg();
			}
		}

		public DiscountsView()
		{
			this.Build();
		}

		private void ConfigureDlg()
		{
			
			buttonAdd.Clicked += (sender, e) => viewModel.AddItemCommand.Execute();
			//buttonAdd.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();

			viewModel.DiscountsList.PropertyChanged += (sender, e) => Redraw();
			//Redraw();
		}

		private void DrawNewRow(Discount newDiscount)
		{
			if(VBoxList?.FirstOrDefault() == null)
				VBoxList = new List<VBox>();
			
			HBox hBox = new HBox();

			var labelTMC = new Label();
			labelTMC.Text = "ТМЦ:";
			hBox.Add(labelTMC);
			hBox.SetChildPacking(labelTMC, true, true, 0, PackType.Start);

			var labelType = new Label();
			labelType.Text = "Тип:";
			hBox.Add(labelType);
			hBox.SetChildPacking(labelType, true, true, 0, PackType.Start);

			var discountTypeCombo = new yEnumComboBox();
			discountTypeCombo.ItemsEnum = typeof(DiscountRowType);
			discountTypeCombo.Binding.AddBinding(newDiscount, e => e.Type, w => w.SelectedItem).InitializeFromSource();
			hBox.Add(discountTypeCombo);
			hBox.SetChildPacking(discountTypeCombo, false, false, 0, PackType.Start);

			var labelDiscounts = new Label();
			labelDiscounts.Text = "Скидки:";
			hBox.Add(labelDiscounts);
			hBox.SetChildPacking(labelDiscounts, true, true, 0, PackType.Start);

			var labelMin = new Label();
			labelMin.Text = "Мин";
			hBox.Add(labelMin);
			hBox.SetChildPacking(labelMin, true, true, 0, PackType.Start);

			var yentryMin = new yValidatedEntry();
			yentryMin.ValidationMode = ValidationType.numeric;
			yentryMin.Binding.AddBinding(newDiscount, e => e.DiscountMinValue, w => w.Text, new IntToStringConverter()).InitializeFromSource();
			yentryMin.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(yentryMin);
			hBox.SetChildPacking(yentryMin, false, false, 0, PackType.Start);

			var labelMax = new Label();
			labelMax.Text = "Макс";
			hBox.Add(labelMax);
			hBox.SetChildPacking(labelMax, true, true, 0, PackType.Start);

			var yentryMax = new yValidatedEntry();
			yentryMin.ValidationMode = ValidationType.numeric;
			yentryMax.Binding.AddBinding(newDiscount, e => e.DiscountMaxValue, w => w.Text, new IntToStringConverter()).InitializeFromSource();
			yentryMax.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(yentryMax);
			hBox.SetChildPacking(yentryMax, false, false, 0, PackType.Start);

			var discountValueTypeCombo = new yEnumComboBox();
			discountValueTypeCombo.ItemsEnum = typeof(DiscountValueType);
			discountValueTypeCombo.Binding.AddBinding(newDiscount, e => e.Type, w => w.SelectedItem).InitializeFromSource();
			hBox.Add(discountValueTypeCombo);
			hBox.SetChildPacking(discountValueTypeCombo, false, false, 0, PackType.Start);

			var labelQuantity = new Label();
			labelQuantity.Text = "Кол-во:";
			hBox.Add(labelQuantity);
			hBox.SetChildPacking(labelQuantity, true, true, 0, PackType.Start);

			var labelMinQ = new Label();
			labelMinQ.Text = "Мин";
			hBox.Add(labelMinQ);
			hBox.SetChildPacking(labelMinQ, true, true, 0, PackType.Start);

			var yentryMinQuality = new yValidatedEntry();
			yentryMinQuality.ValidationMode = ValidationType.numeric;
			yentryMinQuality.Binding.AddBinding(newDiscount, e => e.DiscountMinValue, w => w.Text, new IntToStringConverter()).InitializeFromSource();
			yentryMinQuality.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(yentryMinQuality);
			hBox.SetChildPacking(yentryMinQuality, false, false, 0, PackType.Start);

			var labelMaxQ = new Label();
			labelMaxQ.Text = "Макс";
			hBox.Add(labelMaxQ);
			hBox.SetChildPacking(labelMaxQ, true, true, 0, PackType.Start);

			var yentryMaxQuality = new yValidatedEntry();
			yentryMaxQuality.ValidationMode = ValidationType.numeric;
			yentryMaxQuality.Binding.AddBinding(newDiscount, e => e.DiscountMaxValue, w => w.Text, new IntToStringConverter()).InitializeFromSource();
			yentryMaxQuality.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(yentryMaxQuality);
			hBox.SetChildPacking(yentryMaxQuality, false, false, 0, PackType.Start);

			hBox.ShowAll();

			VBox vBoxAll = new VBox();
			vBoxAll.Add(hBox);

			HBox hBoxButtons = new HBox();

			var buttonAnd = new yButton();
			buttonAnd.Label = "И";
			hBoxButtons.Add(buttonAnd);
			hBoxButtons.SetChildPacking(buttonAnd, false, false, 0, PackType.Start);

			var buttonOr = new yButton();
			buttonOr.Label = "ИЛИ";
			hBoxButtons.Add(buttonOr);
			hBoxButtons.SetChildPacking(buttonOr, false, false, 0, PackType.Start);

			hBoxButtons.ShowAll();
			vBoxAll.Add(hBoxButtons);

			vBoxAll.ShowAll();

			//vboxDiscounts.Add(hBox);
			vboxDiscounts.Add(vBoxAll);
			vboxDiscounts.ShowAll();

			VBoxList.Add(vBoxAll);
		}

		private void Redraw()
		{
			foreach(var child in vboxDiscounts.Children)
			{
				vboxDiscounts.Remove(child);
			}

			foreach(Discount discount in viewModel.DiscountsList)
			{
				DrawNewRow(discount);
			}
		}
	}
}
