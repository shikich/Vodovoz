using System;
using System.Collections.Generic;
using Autofac;
using Gamma.Utilities;
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
using Vodovoz.Domain.Logistic;
using Vodovoz.ViewModels.Journals.Filters.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ReportsParameters.Logistic
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class AddressesOverpaymentsReport : SingleUoWWidgetBase, IParametersWidget
	{
		private readonly IInteractiveService _interactiveService;
		private readonly ILifetimeScope _scope;
		private readonly INavigationManager _navigationManager;
		private readonly ITdiTab _parrentDialog;
		private IEntityEntryViewModel _driverViewModel;
		private IEntityEntryViewModel _logisticianViewModel;

		public AddressesOverpaymentsReport(
			IInteractiveService interactiveService,
			ILifetimeScope scope,
			INavigationManager navigationManager,
			ITdiTab parrentDialog)
		{
			_interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
			_parrentDialog = parrentDialog ?? throw new ArgumentNullException(nameof(parrentDialog));
			this.Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			Configure();
		}

		public string Title => "Отчет по переплатам за адрес";
		public event EventHandler<LoadReportEventArgs> LoadReport;

		private void Configure()
		{
			buttonInfo.Clicked += ShowInfoWindow;
			buttonRun.Clicked += OnButtonRunClicked;
			buttonRun.Sensitive = false;
			datePicker.StartDateChanged += (sender, e) => { buttonRun.Sensitive = true; };
			
			comboDriverOf.ItemsEnum = typeof(CarTypeOfUse);
			comboDriverOf.ChangedByUser += (sender, args) => OnDriverOfSelected();
			
			ConfigureEntries();
		}
		
		private void ConfigureEntries()
		{
			var builder = new LegacyEEVMBuilderFactory(_parrentDialog, UoW, _navigationManager, _scope);

			_driverViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel, EmployeeFilterViewModel>(
					x => x.RestrictCategory = EmployeeCategory.driver)
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();

			driverEntry.ViewModel = _driverViewModel;
			_driverViewModel.Changed += (sender, args) => OnEmployeeSelected();
			
			_logisticianViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel, EmployeeFilterViewModel>(
					x => x.Category = EmployeeCategory.office,
					x => x.Status = EmployeeStatus.IsWorking)
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();

			logisticianEntry.ViewModel = _logisticianViewModel;
		}

		private void OnButtonRunClicked(object sender, EventArgs e)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo()));
		}

		private ReportInfo GetReportInfo()
		{
			return new ReportInfo
			{
				Identifier = "Logistic.AddressesOverpaymentsReport",
				Parameters = new Dictionary<string, object>
				{
					{"start_date", datePicker.StartDateOrNull},
					{"end_date", datePicker.EndDateOrNull?.AddHours(23).AddMinutes(59).AddSeconds(59)},
					{"creation_date", DateTime.Now},
					{"driver_of", comboDriverOf.SelectedItemOrNull},
					{"employee_id", _driverViewModel.Entity.GetIdOrNull()},
					{"logistician_id", _logisticianViewModel.Entity.GetIdOrNull()},
					{"filters", GetSelectedFilters()}
				}
			};
		}

		private string GetSelectedFilters()
		{
			var filters = "Фильтры: водитель: ";
			var empl = _driverViewModel.GetEntity<Employee>();
			if (empl != null)
			{
				filters += $"{empl.ShortName}";
				filters += $", управляет: {empl.DriverOf.GetEnumTitle()}";
			}
			else
			{
				filters += "все, управляет а/м: ";
				var driver_of = comboDriverOf.SelectedItemOrNull == null
					? "все"
					: ((CarTypeOfUse)comboDriverOf.SelectedItem).GetEnumTitle();
				filters += driver_of;
			}
			var logistician = _logisticianViewModel.GetEntity<Employee>();
			filters += ", логист: ";
			if (logistician != null)
			{
				filters += $"{logistician.ShortName}";
			}
			else
			{
				filters += "все";
			}
			return filters;
		}

		private void ShowInfoWindow(object sender, EventArgs args)
		{
			var info = "Особенности отчета:\n" + 
			           "<b>Ситуация 1.</b> У водителя не было закрепленных районов доставки на некоторую дату, и он закрыл какой-либо МЛ " +
			           "в этот день.\n" +
			           "З/П по адресам этого МЛ начислится как за чужой район, и заказы появятся в данном отчете, в \"закрепленных " +
			           "районах\" будет пусто.\n" +
			           "В тот же день водителю добавили районы доставки, в таком случае (у водителя их еще не было), районы активируются " +
			           "сразу \n" +
			           "в 00 часов 00 минут того же дня, а не на следующий день. Итог - после повторной генерации отчета \n" +
			           "у заказов появятся \"закрепленные районы\", которые, к тому же, могут совпасть с \"районом доставки\".\n" +
			           "<b>Ситуация 2.</b> У некоторого водителя ставка за свой и чужой районы на момент выполнения МЛ может совпадать.\n" +
			           "В отчет попадают заказы, у которых переплата больше нуля.\n" +
			           "Затем водителю сделали разницу в ставках за районы. Отчет отобразит заказы, если после изменения ставок ЗП за МЛ " +
			           "будет пересчитана.\n\n" +
					  "Сокращения отчета:\n" +
					  "<b>КТС</b>: категория транспортного средства. \n\tСокращения столбца: " +
					  "<b>К</b>: транспорт компании , <b>Н</b>: наемный транспорт, <b>Л</b>: ларгус, <b>Ф</b>: фура, <b>Г</b>: газель.\n\n" +
					  "Столбцы отчета:\n" +
					  "<b>№</b>: порядковый номер\n" +
					  "<b>№ МЛ</b>: номер маршрутного листа\n" +
					  "<b>№ заказа</b>: номер заказа маршрутного листа\n" +
					  "<b>ФИО водителя</b>: фамилия имя отчество водителя\n" +
					  "<b>ФИО логиста</b>: фамилия имя отчество логиста, создавшего МЛ\n" +
					  "<b>КТС</b>: вид транспорта, которым управляет водитель\n" +
					  "<b>Подразделение</b>: подразделение водителя\n" +
					  "<b>Адрес</b>: адрес в чужом для водителя районе\n" +
					  "<b>Переплата</b>: разница между ставкой за 1 адрес в чужом районе и ставкой за свой район\n" +
					  "<b>Район адреса</b>\n" +
					  "<b>Закрепленные районы</b>: закрепленные за водителем районы на дату МЛ\n" +
					  "<b>Комментарий</b>: комментарий к адресу из диалога Разбор МЛ";

			_interactiveService.ShowMessage(ImportanceLevel.Info, info, "Информация");
		}

		private void OnDriverOfSelected()
		{
			if(comboDriverOf.SelectedItemOrNull != null)
			{
				driverEntry.Sensitive = false;
				_driverViewModel.Entity = null;
			}
			else
			{
				driverEntry.Sensitive = true;
			}
		}

		private void OnEmployeeSelected()
		{
			if(_driverViewModel.Entity is Employee empl)
			{
				if(empl.Category != EmployeeCategory.driver)
				{
					_interactiveService.ShowMessage(ImportanceLevel.Warning, "Можно выбрать только водителя");
					_driverViewModel.Entity = null;
					return;
				}

				comboDriverOf.Sensitive = false;
				comboDriverOf.SelectedItemOrNull = empl.DriverOf;
			}
			else
			{
				comboDriverOf.Sensitive = true;
			}
		}
	}
}
