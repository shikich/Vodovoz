using System;
using System.Collections.Generic;
using Autofac;
using QS.Dialog;
using QS.Dialog.GtkUI;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Report;
using QS.Tdi;
using QS.ViewModels.Control.EEVM;
using QSReport;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ReportsParameters.Bottles
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ReturnedTareReport : SingleUoWWidgetBase, IParametersWidget
	{
		private readonly IInteractiveService _interactiveService;
		private readonly ILifetimeScope _scope;
		private readonly INavigationManager _navigationManager;
		private readonly ITdiTab _parrentDialog;
		private IEntityEntryViewModel _authorViewModel;

		public ReturnedTareReport(
			IInteractiveService interactiveService,
			ILifetimeScope scope,
			INavigationManager navigationManager,
			ITdiTab parrentDialog)
		{
			_interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
			_parrentDialog = parrentDialog ?? throw new ArgumentNullException(nameof(parrentDialog));
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			
			Build();
			btnCreateReport.Clicked += (sender, e) => OnUpdate(true);
			btnCreateReport.Sensitive = false;
			daterangepicker.PeriodChangedByUser += Daterangepicker_PeriodChangedByUser;
			yenumcomboboxDateType.ItemsEnum = typeof(OrderDateType);
			yenumcomboboxDateType.SelectedItem = OrderDateType.CreationDate;
			ConfigureEntry();
			
			buttonHelp.Clicked += OnButtonHelpClicked;
		}

		private void ConfigureEntry()
		{
			//TODO
			var builder = new LegacyEEVMBuilderFactory(_parrentDialog, UoW, _navigationManager, _scope);

			_authorViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel>()
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			authorEntry.ViewModel = _authorViewModel;
			//authorEntry.ViewModel.IsEditable = false;
		}

		private void OnButtonHelpClicked(object sender, EventArgs e)
		{
			var info =
				"В отчёт попадают заказы с учётом выбранных фильтров, а также следующих условий:\n" +
				"- есть возвращённые бутыли\n" +
				"- отстуствуют тмц категории \"Вода\" с объёмом тары 19л.";

			_interactiveService.ShowMessage(ImportanceLevel.Info, info, "Информация");
		}

		void Daterangepicker_PeriodChangedByUser(object sender, EventArgs e) =>
			btnCreateReport.Sensitive = daterangepicker.EndDateOrNull.HasValue && daterangepicker.StartDateOrNull.HasValue;


		#region IParametersWidget implementation

		public string Title => "Отчет по забору тары";

		public event EventHandler<LoadReportEventArgs> LoadReport;

		#endregion

		private ReportInfo GetReportInfo()
		{
			return new ReportInfo
			{
				Identifier = "Bottles.ReturnedTareReport",
				Parameters = new Dictionary<string, object>
				{
					{"start_date", daterangepicker.StartDate},
					{"end_date", daterangepicker.EndDate.AddHours(23).AddMinutes(59).AddSeconds(59)},
					{"date", DateTime.Now},
					{"date_type", ((OrderDateType) yenumcomboboxDateType.SelectedItem) == OrderDateType.CreationDate},
					{"author_employee_id", _authorViewModel.Entity.GetIdOrNull()},
					{"author_employee_name", _authorViewModel.GetEntity<Employee>()?.FullName},
					{"is_closed_order_only", chkClosedOrdersOnly.Active}
				}
			};
		}

		void OnUpdate(bool hide = false) => LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
	}
}
