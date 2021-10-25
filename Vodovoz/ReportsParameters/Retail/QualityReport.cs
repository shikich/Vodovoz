using System;
using System.Collections.Generic;
using Autofac;
using QS.Dialog;
using QS.Dialog.GtkUI;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using QS.Report;
using QS.Tdi;
using QS.ViewModels.Control.EEVM;
using QSReport;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Retail;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Journals.FilterViewModels.Retail;
using Vodovoz.ViewModels.Journals.JournalViewModels.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Retail;
using Vodovoz.ViewModels.ViewModels.Employees;

namespace Vodovoz.ReportsParameters.Retail
{
    public partial class QualityReport : SingleUoWWidgetBase, IParametersWidget
    {
		private readonly ILifetimeScope _scope;
		private readonly INavigationManager _navigationManager;
		private readonly IInteractiveService interactiveService;
		private readonly ITdiTab _parrentDialog;
		private IEntityEntryViewModel _mainContactViewModel;
        
        public QualityReport(
            ILifetimeScope scope,
			INavigationManager navigationManager,
            IUnitOfWorkFactory unitOfWorkFactory,
            IInteractiveService interactiveService,
			ITdiTab parrentDialog)
        {
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));
			_navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
			this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
			_parrentDialog = parrentDialog ?? throw new ArgumentNullException(nameof(parrentDialog));
			this.Build();
            UoW = unitOfWorkFactory.CreateWithoutRoot();
            Configure();
        }

        private void Configure()
        {
            buttonCreateReport.Clicked += (sender, e) => OnUpdate(true);
			//На переходный период создаю здесь, чтобы не делать новых фабрик
            yEntityCounterParty.SetEntityAutocompleteSelectorFactory(
				new EntityAutocompleteSelectorFactory<RetailCounterpartyJournalViewModel>(
					typeof(Counterparty),
					() =>
					{
						var newScope = _scope.BeginLifetimeScope();
						return newScope.Resolve<RetailCounterpartyJournalViewModel>();
					}));
			yEntitySalesChannel.SetEntityAutocompleteSelectorFactory(
				new DefaultEntityAutocompleteSelectorFactory<SalesChannel, SalesChannelJournalViewModel,
				SalesChannelJournalFilterViewModel>(ServicesConfig.CommonServices));
			ConfigureEmployeeEntry();
        }

		private void ConfigureEmployeeEntry()
		{
			//TODO
			var builder = new LegacyEEVMBuilderFactory(_parrentDialog, UoW, _navigationManager, _scope);

			_mainContactViewModel = builder.ForEntity<Employee>()
				.UseViewModelJournalAndAutocompleter<EmployeesJournalViewModel>()
				.UseViewModelDialog<EmployeeViewModel>()
				.Finish();
			
			mainContactEntry.ViewModel = _mainContactViewModel;
		}

		private ReportInfo GetReportInfo()
        {
            var parameters = new Dictionary<string, object> {
                { "create_date", ydateperiodpickerCreate.StartDateOrNull },
                { "end_date", ydateperiodpickerCreate.EndDateOrNull?.AddDays(1).AddSeconds(-1) },
                { "shipping_start_date", ydateperiodpickerShipping.StartDateOrNull },
                { "shipping_end_date", ydateperiodpickerShipping.EndDateOrNull?.AddDays(1).AddSeconds(-1) },
                { "counterparty_id", yEntityCounterParty.Subject.GetIdOrNull() ?? 0 },
                { "sales_channel_id", yEntitySalesChannel.Subject.GetIdOrNull() ?? 0 },
                { "main_contact_id", _mainContactViewModel.Entity.GetIdOrNull() ?? 0 }
            };

            return new ReportInfo
            {
                Identifier = "Retail.QualityReport",
                Parameters = parameters
            };
        }

        public string Title => $"Качественный отчет";

        public event EventHandler<LoadReportEventArgs> LoadReport;

        void OnUpdate(bool hide = false)
        {
            if (Validate())
            {
                LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
            }
        }

        bool Validate()
        {
            string errorString = string.Empty;
            if (!(ydateperiodpickerCreate.StartDateOrNull.HasValue &&
                ydateperiodpickerCreate.EndDateOrNull.HasValue) &&
                !(ydateperiodpickerShipping.StartDateOrNull.HasValue && 
                  ydateperiodpickerShipping.EndDateOrNull.HasValue))
            {
                errorString = "Не выбраны периоды";
                interactiveService.ShowMessage(ImportanceLevel.Error, errorString);
                return false;
            }

            return true;
        }
    }
}
