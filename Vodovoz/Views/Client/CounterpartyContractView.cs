using QS.Navigation;
using QS.Project.Services;
using QS.Views.Dialog;
using Vodovoz.DocTemplates;
using Vodovoz.Domain.Client;
using Vodovoz.ViewModels.ViewModels.Counterparties;

namespace Vodovoz.Views.Client
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CounterpartyContractView : DialogViewBase<CounterpartyContractViewModel>
    {
        public CounterpartyContractView(CounterpartyContractViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            buttonSave.Clicked += (sender, args) => ViewModel.SaveCommand.Execute();
            buttonCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);
            
            checkOnCancellation.Binding.AddBinding (ViewModel.Entity, e => e.OnCancellation, w => w.Active).InitializeFromSource();
            checkArchive.Binding.AddBinding (ViewModel.Entity, e => e.IsArchive, w => w.Active).InitializeFromSource();

            dateIssue.Binding.AddBinding (ViewModel.Entity, e => e.IssueDate, w => w.Date).InitializeFromSource();
            entryNumber.Binding.AddBinding (ViewModel.Entity, e => e.ContractFullNumber, w => w.Text).InitializeFromSource();
            spinDelay.Binding.AddBinding (ViewModel.Entity, e => e.MaxDelay, w => w.ValueAsInt).InitializeFromSource();
            ycomboContractType.ItemsEnum = typeof(ContractType);
            ycomboContractType.Binding.AddBinding(ViewModel.Entity, e => e.ContractType, w => w.SelectedItem).InitializeFromSource();

            referenceOrganization.SubjectType = typeof(Domain.Organizations.Organization);
            referenceOrganization.Binding.AddBinding (ViewModel.Entity, e => e.Organization, w => w.Subject).InitializeFromSource();
            referenceOrganization.Binding.AddBinding (ViewModel, vm => vm.OrganisationSensitivity, w => w.Sensitive).InitializeFromSource();

            if (ViewModel.Entity.DocumentTemplate == null && ViewModel.Entity.Organization != null)
                ViewModel.Entity.UpdateContractTemplate(ViewModel.UoW);

            if (ViewModel.Entity.DocumentTemplate != null)
                (ViewModel.Entity.DocumentTemplate.DocParser as ContractParser).RootObject = ViewModel.Entity;

            templatewidget1.CanRevertCommon = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_set_common_additionalagreement");
            templatewidget1.Binding.AddBinding(ViewModel.Entity, e => e.DocumentTemplate, w => w.Template).InitializeFromSource();
            templatewidget1.Binding.AddBinding(ViewModel.Entity, e => e.ChangedTemplateFile, w => w.ChangedDoc).InitializeFromSource();

            entryNumber.Sensitive = false;
            dateIssue.Sensitive = false;
            referenceOrganization.Sensitive = false;
            ycomboContractType.Sensitive = false;
        }
    }
}
