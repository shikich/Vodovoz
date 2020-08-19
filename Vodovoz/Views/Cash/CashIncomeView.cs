using Gamma.ColumnConfig;
using QS.Project.Services;
using QS.Views.GtkUI;
using Vodovoz.Domain.Cash;
using Vodovoz.Domain.Logistic;
using Vodovoz.ViewModels.Dialogs.Cash;
using VodovozInfrastructure.Extensions;

namespace Vodovoz.Views.Cash
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CashIncomeView : TabViewBase<CashIncomeViewModel>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public CashIncomeView(CashIncomeViewModel viewModel) : base(viewModel)
		{
			this.Build();
			ConfigureView();
		}

		private void ConfigureView()
		{
			enumcomboOperation.ItemsEnum = typeof(IncomeType);
			enumcomboOperation.Binding.AddBinding(ViewModel, vm => vm.IncomeType, w => w.SelectedItem).InitializeFromSource();

			entryCashier.SetEntityAutocompleteSelectorFactory(ViewModel.EmployeesSelectorFactory);
			entryCashier.Binding.AddBinding(ViewModel.Entity, e => e.Casher, w => w.Subject).InitializeFromSource();

			entryEmployee.SetEntityAutocompleteSelectorFactory(ViewModel.EmployeesSelectorFactory);
			entryEmployee.Binding.AddBinding(ViewModel.Entity, e => e.Casher, w => w.Subject).InitializeFromSource();

			//FIXME Исправить на новый журнал когда перепишем маршрутные листы
			var filterRL = new RouteListsFilter(ViewModel.UoW);
			filterRL.OnlyStatuses = new RouteListStatus[] { RouteListStatus.EnRoute, RouteListStatus.OnClosing };
			yEntryRouteList.RepresentationModel = new ViewModel.RouteListsVM(filterRL);
			yEntryRouteList.Binding.AddBinding(ViewModel.Entity, s => s.RouteListClosing, w => w.Subject).InitializeFromSource();
			yEntryRouteList.CanEditReference = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_delete");

			entryClient.SetEntityAutocompleteSelectorFactory(ViewModel.CounterpartySelectorFactory);
			entryClient.Binding.AddBinding(ViewModel.Entity, e => e.Customer, w => w.Subject).InitializeFromSource();

			ydateDocument.Binding.AddBinding(ViewModel.Entity, s => s.Date, w => w.Date).InitializeFromSource();

			comboExpenseCategory.ItemsList = ViewModel.ExpenseCategories;
			comboExpenseCategory.Binding.AddBinding(ViewModel.Entity, s => s.ExpenseCategory, w => w.SelectedItem).InitializeFromSource();

			comboIncomeCategory.ItemsList = ViewModel.IncomeCategories;
			comboIncomeCategory.Binding.AddBinding(ViewModel.Entity, s => s.IncomeCategory, w => w.SelectedItem).InitializeFromSource();

			checkNoClose.Binding.AddBinding(ViewModel.Entity, e => e.IsPartialDebtReturn, w => w.Active);

			yspinMoney.Binding.AddBinding(ViewModel.Entity, s => s.Money, w => w.ValueAsDecimal).InitializeFromSource();

			ytextviewDescription.Binding.AddBinding(ViewModel.Entity, s => s.Description, w => w.Buffer.Text).InitializeFromSource();

			buttonPrint.Clicked += (s, e) => ViewModel.PrintCommand.Execute();
			ViewModel.PrintCommand.CanExecuteChanged += (sender, e) => buttonPrint.Sensitive = ViewModel.PrintCommand.CanExecute();


			ytreeviewDebts.ColumnsConfig = FluentColumnsConfig<SelectableNode<Expense>>.Create()
				.AddColumn("Закрыть").AddToggleRenderer(a => a.Selected).Editing()
				.AddColumn("Дата").AddTextRenderer(a => a.Value.Date.ToString())
				.AddColumn("Получено").AddTextRenderer(a => a.Value.Money.ToString("C"))
				.AddColumn("Непогашено").AddTextRenderer(a => a.Value.UnclosedMoney.ToString("C"))
				.AddColumn("Статья").AddTextRenderer(a => a.Value.ExpenseCategory.Name)
				.AddColumn("Основание").AddTextRenderer(a => a.Value.Description)
				.Finish();
			ytreeviewDebts.ItemsDataSource = ViewModel.Entity.Debts;
		}
	}
}
