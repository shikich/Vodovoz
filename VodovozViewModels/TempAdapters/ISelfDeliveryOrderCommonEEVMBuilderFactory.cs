using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.ViewModels.Control.EEVM;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.TempAdapters
{
    public interface ISelfDeliveryOrderCommonEEVMBuilderFactory
    {
        CommonEEVMBuilderFactory<SelfDeliveryOrder> CreateSelfDeliveryOrderCommonEEVMBuilderFactory(
            DialogViewModelBase parentVM,
            SelfDeliveryOrder source,
            IUnitOfWork uow,
            INavigationManager navigationManager,
            ILifetimeScope autofacScope);
    }
}