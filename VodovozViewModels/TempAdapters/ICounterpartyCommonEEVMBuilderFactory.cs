using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.ViewModels.Control.EEVM;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewModels.TempAdapters
{
    public interface ICounterpartyCommonEEVMBuilderFactory
    {
        CommonEEVMBuilderFactory<Counterparty> CreateCounterpartyCommonEEVMBuilderFactory(
            DialogViewModelBase parentVM,
            Counterparty source,
            IUnitOfWork uow,
            INavigationManager navigationManager,
            ILifetimeScope autofacScope);
    }
}