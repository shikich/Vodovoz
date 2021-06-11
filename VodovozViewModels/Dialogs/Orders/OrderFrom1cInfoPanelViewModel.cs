using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Services;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderFrom1cInfoPanelViewModel : OrderInfoPanelViewModelBase
    {
        public OrderFrom1c Order { get; set; }
        
        public OrderFrom1cInfoPanelViewModel(
            ICommonServices commonServices,
            IOrderRepository orderRepository,
            IOrderParametersProvider orderParametersProvider,
            OrderFrom1c order) : base(commonServices, orderRepository, orderParametersProvider)
        {
            Order = order;

            UoW = UnitOfWorkFactory.CreateWithoutRoot();
        }
    }
}