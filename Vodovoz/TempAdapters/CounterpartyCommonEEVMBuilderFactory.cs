using Autofac;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Tdi;
using QS.ViewModels.Control.EEVM;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Client;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.TempAdapters
{
    public class CounterpartyCommonEEVMBuilderFactory : ICounterpartyCommonEEVMBuilderFactory
    {
        public CommonEEVMBuilderFactory<Counterparty> CreateCounterpartyCommonEEVMBuilderFactory(
            DialogViewModelBase parentVM,
            Counterparty source,
            IUnitOfWork uow,
            INavigationManager navigationManager, 
            ILifetimeScope autofacScope)
        {
            return new CommonEEVMBuilderFactory<Counterparty>(
                parentVM, 
                source, 
                uow,
                navigationManager,
                autofacScope);
        }
    }
    
    public interface ICounterpartyLegacyEEVMBuilderFactory
    {
        LegacyEEVMBuilderFactory<Counterparty> CreateCounterpartyLegacyEEVMBuilderFactory(
            ITdiTab parentTab,
            Counterparty source,
            IUnitOfWork uow,
            INavigationManager navigationManager,
            ILifetimeScope autofacScope);
    }
    
    public class CounterpartyLegacyEEVMBuilderFactory : ICounterpartyLegacyEEVMBuilderFactory
    {
        public LegacyEEVMBuilderFactory<Counterparty> CreateCounterpartyLegacyEEVMBuilderFactory(
            ITdiTab parentTab,
            Counterparty source,
            IUnitOfWork uow,
            INavigationManager navigationManager,
            ILifetimeScope autofacScope)
        {
            return new LegacyEEVMBuilderFactory<Counterparty>(
                parentTab, 
                source, 
                uow,
                navigationManager,
                autofacScope);
        }
    }
}