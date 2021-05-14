using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Validation;
using QS.ViewModels.Dialog;
using Vodovoz.Domain;

namespace Vodovoz.ViewModels.ViewModels.Rent
{
    public class PaidRentPackageViewModel : EntityDialogViewModelBase<PaidRentPackage>
    {
        public PaidRentPackageViewModel(
            IEntityUoWBuilder uowBuilder,
            IUnitOfWorkFactory unitOfWorkFactory,
            INavigationManager navigation,
            IValidator validator) : base(uowBuilder, unitOfWorkFactory, navigation, validator)
        {
        }
    }
}
