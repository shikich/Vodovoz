﻿using QS.Views.GtkUI;
using System;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.Utilities;
using QS.ViewModels;
using QS.Views.Dialog;
using Vodovoz.ViewModels.Reports;
using Alignment = Pango.Alignment;

namespace Vodovoz.Views.Reports
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ProductionWarehouseMovementReportView : TabViewBase<ProductionWarehouseMovementReportViewModel>
	{
		public ProductionWarehouseMovementReportView(ProductionWarehouseMovementReportViewModel viewModel) : base(viewModel)
		{
			Build();
			Configure();
		}

		private void Configure()
		{
			ybtnExport.Clicked += (sender, args) => ViewModel.ExportCommand.Execute();
			ybtnRunReport.Clicked += (sender, args) => RunReport();
			btnHelp.Clicked += (sender, args) => ViewModel.HelpCommand.Execute();

			cmbProducionWarehouse.SetRenderTextFunc<Domain.Store.Warehouse>(x => x.Name);
			cmbProducionWarehouse.Binding
				.AddSource(ViewModel)
				.AddBinding(vm => vm.WarehouseList, w => w.ItemsList)
				.AddBinding(vm => vm.FilterWarehouse, w => w.SelectedItem)
				.InitializeFromSource();

			rangepickerWarehouseDocumentDate.Binding.AddSource(ViewModel)
				.AddBinding(vm => vm.FilterStartDate, w => w.StartDateOrNull)
				.AddBinding(vm => vm.FilterEndDate, w => w.EndDateOrNull)
				.InitializeFromSource();

			ycheckbtnDetails.Binding.AddBinding(ViewModel, vm => vm.IsDetailed, w => w.Active).InitializeFromSource();
			ybtnRunReport.Binding.AddBinding(ViewModel, vm => vm.CanGenerate, w => w.Sensitive).InitializeFromSource();
		}

		private void RunReport()
		{
			ViewModel.RunReportCommand.Execute();
			ConfigureTree();
		}

		private void ConfigureTree()
		{
			if(ViewModel.IsDetailed)
			{
				var columnsConfig = Gamma.ColumnConfig.FluentColumnsConfig<ProductionWarehouseMovementReport.ProductionWarehouseMovementReportNode>.Create()
					.AddColumn("Дата")
					.AddTextRenderer(n => n.MovementDocumentDate > DateTime.MinValue ? n.MovementDocumentDate.ToShortDateString() : String.Empty)
					.AddColumn("ТМЦ")
					.AddTextRenderer(n => n.MovementDocumentName);

				for(var i = 0; i < ViewModel.Report.Titles?.Count; i++)
				{
					var index = i;

					columnsConfig.AddColumn($"{ ViewModel.Report.Titles[index].NomenclatureName }")
						.MinWidth(70)
						.AddNumericRenderer((row => row.NomenclatureColumns[index].Amount)).Digits(2);
				}

				ytreeviewReport.ColumnsConfig = columnsConfig.Finish();
				ytreeviewReport.ItemsDataSource = ViewModel.Report.ResultNodeList;
			}
			else
			{
				var columnsConfig = Gamma.ColumnConfig
					.FluentColumnsConfig<ProductionWarehouseMovementReport.ProductionWarehouseMovementReportNomenclature>.Create()
					.AddColumn("ТМЦ")
					.AddTextRenderer(n => n.NomenclatureName)
					.AddColumn("Период")
					.AddTextRenderer(n => n.DateRange)
					.AddColumn("Цена")
					.AddNumericRenderer(n => n.PurchasePrice).Digits(2)
					.AddColumn("Количество")
					.AddNumericRenderer(n => n.Amount).Digits(2)
					.AddColumn("Сумма")
					.AddNumericRenderer(n => n.Sum).Digits(2);

				ytreeviewReport.ColumnsConfig = columnsConfig.Finish();
				ytreeviewReport.ItemsDataSource = ViewModel.Report.ResultNodeList.SelectMany(x => x.NomenclatureColumns).ToList();
			}

			ytreeviewReport.EnableGridLines = TreeViewGridLines.Both;
		}
	}
}
