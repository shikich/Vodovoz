using NHibernate.Criterion;
using QS.Navigation;
using QS.Views.Dialog;
using Vodovoz.Domain;
using Vodovoz.Domain.Goods;
using Vodovoz.ViewModels.ViewModels.Rent;

namespace Vodovoz.Views.Rent
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class FreeRentPackageView : DialogViewBase<FreeRentPackageViewModel>
    {
        public FreeRentPackageView(FreeRentPackageViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            buttonSave.Clicked += (sender, e) => ViewModel.SaveAndClose();
            buttonCancel.Clicked += (sender, e) => ViewModel.Close(false, CloseSource.Cancel);

            dataentryName.Binding.AddBinding(ViewModel.Entity, e => e.Name, w => w.Text).InitializeFromSource();
            spinDeposit.Binding.AddBinding(ViewModel.Entity, e => e.Deposit, w => w.ValueAsDecimal).InitializeFromSource();
            spinMinWaterAmount.Binding.AddBinding(ViewModel.Entity, e => e.MinWaterAmount, w => w.ValueAsInt).InitializeFromSource();

            referenceDepositService.SubjectType = typeof(Nomenclature);
            referenceDepositService.ItemsCriteria = ViewModel.UoW.Session.CreateCriteria<Nomenclature>()
                .Add(Restrictions.Eq("Category", NomenclatureCategory.deposit));
            referenceDepositService.Binding.AddBinding(ViewModel.Entity, e => e.DepositService, w => w.Subject).InitializeFromSource();
            referenceEquipmentType.SubjectType = typeof(EquipmentType);
            referenceEquipmentType.Binding.AddBinding(ViewModel.Entity, e => e.EquipmentType, w => w.Subject).InitializeFromSource();
        }
    }
}
