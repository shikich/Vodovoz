using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.ViewModels.Control.EEVM;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.TempAdapters
{
    public class SelfDeliveryOrderCommonEEVMBuilderFactory : ISelfDeliveryOrderCommonEEVMBuilderFactory
    {
        public CommonEEVMBuilderFactory<SelfDeliveryOrder> CreateSelfDeliveryOrderCommonEEVMBuilderFactory(
            DialogViewModelBase parentVM,
            SelfDeliveryOrder source,
            IUnitOfWork uow,
            INavigationManager navigationManager, 
            ILifetimeScope autofacScope)
        {
            return new CommonEEVMBuilderFactory<SelfDeliveryOrder>(
                parentVM, 
                source, 
                uow,
                navigationManager,
                autofacScope);
        }
    }
}