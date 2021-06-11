using NHibernate.Criterion;
using QS.Navigation;
using QS.Views.Dialog;
using Vodovoz.Domain;
using Vodovoz.Domain.Goods;
using Vodovoz.ViewModels.ViewModels.Rent;
namespace Vodovoz.Views.Rent
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PaidRentPackageView : DialogViewBase<PaidRentPackageViewModel>
    {
        public PaidRentPackageView(PaidRentPackageViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            buttonSave.Clicked += (sender, args) => ViewModel.SaveAndClose();
            buttonCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);
            
            dataentryName.Binding.AddBinding(ViewModel.Entity, e => e.Name, w => w.Text).InitializeFromSource();
            spinDeposit.Binding.AddBinding(ViewModel.Entity, e => e.Deposit, w => w.ValueAsDecimal).InitializeFromSource();
            spinPriceDaily.Binding.AddBinding(ViewModel.Entity, e => e.PriceDaily, w => w.ValueAsDecimal).InitializeFromSource();
            spinPriceMonthly.Binding.AddBinding(ViewModel.Entity, e => e.PriceMonthly, w => w.ValueAsDecimal).InitializeFromSource();

            referenceDepositService.SubjectType = typeof(Nomenclature);
            referenceDepositService.ItemsCriteria = ViewModel.UoW.Session.CreateCriteria<Nomenclature>()
                .Add(Restrictions.Eq("Category", NomenclatureCategory.deposit));
            referenceDepositService.Binding.AddBinding(ViewModel.Entity, e => e.DepositService, w => w.Subject).InitializeFromSource();

            referenceRentServiceDaily.SubjectType = typeof(Nomenclature);
            referenceRentServiceDaily.ItemsCriteria = ViewModel.UoW.Session.CreateCriteria<Nomenclature>();
            referenceRentServiceDaily.Binding.AddBinding(ViewModel.Entity, e => e.RentServiceDaily, w => w.Subject).InitializeFromSource();

            referenceRentServiceMonthly.SubjectType = typeof(Nomenclature);
            referenceRentServiceMonthly.ItemsCriteria = ViewModel.UoW.Session.CreateCriteria<Nomenclature>();
            referenceRentServiceMonthly.Binding.AddBinding(ViewModel.Entity, e => e.RentServiceMonthly, w => w.Subject).InitializeFromSource();

            referenceEquipmentType.SubjectType = typeof(EquipmentType);
            referenceEquipmentType.Binding.AddBinding(ViewModel.Entity, e => e.EquipmentType, w => w.Subject).InitializeFromSource();
        }
    }
}
