using System;
using System.Linq;
using Gamma.GtkWidgets;
using Gtk;
using QS.Navigation;
using QS.Project.Services;
using QS.Views.Dialog;
using Vodovoz.DocTemplates;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModel;
using Vodovoz.ViewModels.Employees;

namespace Vodovoz.Views.Employees
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class M2ProxyDocumentView : DialogViewBase<M2ProxyDocumentViewModel>
    {
        public M2ProxyDocumentView(M2ProxyDocumentViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        void Configure()
        {
	        buttonSave.Clicked += (sender, args) => ViewModel.SaveCommand.Execute();
	        buttonCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);
			
			ylabelNumber.Binding.AddBinding(ViewModel.Entity, x => x.Title, x => x.LabelProp).InitializeFromSource();
			var filterOrders = new OrdersFilter(ViewModel.UoW);
			yEForOrder.RepresentationModel = new OrdersVM(filterOrders);
			yEForOrder.Binding.AddBinding(ViewModel.Entity, x => x.Order, x => x.Subject).InitializeFromSource();
			yEForOrder.Changed += (sender, e) => ViewModel.FillForOrder();
			
			yEForOrder.CanEditReference = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_delete");

			yentryOrganization.SubjectType = typeof(Domain.Organizations.Organization);
			yentryOrganization.Binding.AddBinding(ViewModel.Entity, x => x.Organization, x => x.Subject).InitializeFromSource();
			yentryOrganization.Changed += (sender, e) => {
				UpdateStates();
			};

			yDPDatesRange.Binding.AddBinding(ViewModel.Entity, x => x.Date, x => x.StartDate).InitializeFromSource();
			yDPDatesRange.Binding.AddBinding(ViewModel.Entity, x => x.ExpirationDate, x => x.EndDate).InitializeFromSource();

			yEEmployee.RepresentationModel = new EmployeesVM();
			yEEmployee.Binding.AddBinding(ViewModel.Entity, x => x.Employee, x => x.Subject).InitializeFromSource();

			var filterSupplier = new CounterpartyFilter(ViewModel.UoW);
			yESupplier.RepresentationModel = new CounterpartyVM(filterSupplier);
			yESupplier.Binding.AddBinding(ViewModel.Entity, x => x.Supplier, x => x.Subject).InitializeFromSource();

			yETicketNr.Binding.AddBinding(ViewModel.Entity, x => x.TicketNumber, w => w.Text).InitializeFromSource();

			yDTicketDate.Binding.AddBinding(ViewModel.Entity, x => x.TicketDate, x => x.DateOrNull).InitializeFromSource();

			RefreshParserRootObject();

			templatewidget.CanRevertCommon = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_set_common_additionalagreement");
			templatewidget.Binding.AddBinding(ViewModel.Entity, e => e.DocumentTemplate, w => w.Template).InitializeFromSource();
			templatewidget.Binding.AddBinding(ViewModel.Entity, e => e.ChangedTemplateFile, w => w.ChangedDoc).InitializeFromSource();

			templatewidget.BeforeOpen += Templatewidget_BeforeOpen;

			yTWEquipment.ColumnsConfig = ColumnsConfigFactory.Create<OrderEquipment>()
				.AddColumn("Наименование")
					.SetDataProperty(node => node.FullNameString)
				.AddColumn("Направление")
					.SetDataProperty(node => node.DirectionString)
				.AddColumn("Кол-во")
					.AddNumericRenderer(node => node.Count)
					.WidthChars(10)
					.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0))
				.AddColumn("")
				.Finish();

			yTWEquipment.ItemsDataSource = ViewModel.EquipmentList;
			UpdateStates();
		}
        
        void UpdateStates()
        {
	        bool isNewDoc = !(ViewModel.Entity.Id > 0);
	        yEForOrder.Sensitive = yDPDatesRange.Sensitive = yEEmployee.Sensitive = yESupplier.Sensitive = yETicketNr.Sensitive
		        = yDTicketDate.Sensitive = yTWEquipment.Sensitive = yentryOrganization.Sensitive = isNewDoc;

	        if(ViewModel.Entity.Organization == null || !isNewDoc) {
		        return;
	        }
	        
	        templatewidget.AvailableTemplates = Repository.Client.DocTemplateRepository.GetAvailableTemplates(
		        ViewModel.UoW, TemplateType.M2Proxy, ViewModel.Entity.Organization);
	        templatewidget.Template = templatewidget.AvailableTemplates.FirstOrDefault();
        }
        
        void Templatewidget_BeforeOpen(object sender, EventArgs e)
        {
	        if(ViewModel.UoW.HasChanges) {
		        if(ViewModel.CommonServices.InteractiveService.Question("Необходимо сохранить документ перед открытием печатной формы, сохранить?")) {
			        ViewModel.UoWGeneric.Save();
			        RefreshParserRootObject();
		        } else {
			        templatewidget.CanOpenDocument = false;
		        }
	        }
        }

        void RefreshParserRootObject()
        {
	        if(ViewModel.Entity.DocumentTemplate == null)
		        return;
			
	        M2ProxyDocumentParser parser = (ViewModel.Entity.DocumentTemplate.DocParser as M2ProxyDocumentParser);
	        parser.RootObject = ViewModel.Entity;
	        parser.AddTableEquipmentFromClient(ViewModel.EquipmentList);
        }
    }
}
