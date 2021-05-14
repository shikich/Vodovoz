using System;
using System.Text;
using NLog;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels.Dialog;
using Vodovoz.Domain;
using QS.Validation;

namespace Vodovoz.ViewModels.ViewModels.Rent
{
    public class FreeRentPackageViewModel : EntityDialogViewModelBase<FreeRentPackage>
    {
        public FreeRentPackageViewModel(
            IEntityUoWBuilder uowBuilder,
            IUnitOfWorkFactory unitOfWorkFactory,
            INavigationManager navigation,
            IValidator validator) : base(uowBuilder, unitOfWorkFactory, navigation, validator)
        {
        }
    }
}
