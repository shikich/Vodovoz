using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Logistic;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using WebAPI.Library.Models;

namespace WebAPI.Library.DataAccess
{
    public class APIRouteListData
    {
        private readonly IRouteListRepository routeListRepository;
        private readonly IOrderRepository orderRepository;

        public APIRouteListData(IRouteListRepository routeListRepository, IOrderRepository orderRepository)
        {
            this.routeListRepository = routeListRepository ?? throw new ArgumentNullException(nameof(routeListRepository));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public IEnumerable<APIRouteList> Get(int[] routeListsIds)
        {
            var vodovozRouteLists = routeListRepository.GetRouteLists(routeListsIds);

            var routeLists = new List<APIRouteList>();

            foreach(var routelist in vodovozRouteLists)
            {
                routeLists.Add(convertToAPIRouteList(routelist));
            }

            return routeLists;
        }

        private APIRouteList convertToAPIRouteList(RouteList routeList)
        {
            var routelistAddresses = new List<APIRouteListAddress>();

            foreach (var address in routeList.Addresses)
            {
                routelistAddresses.Add(convertToAPIRouteListAddress(address));
            }

            return new APIRouteList()
            {
                Id = routeList.Id,
                Status = convertToAPIStatus(routeList.Status),
                RouteListAddresses = routelistAddresses
            };
        }

        private APIRouteListStatus convertToAPIStatus(RouteListStatus routeListStatus)
        {
            switch (routeListStatus)
            {
                case RouteListStatus.New:
                    return APIRouteListStatus.New;
                case RouteListStatus.Confirmed:
                    return APIRouteListStatus.Confirmed;
                case RouteListStatus.InLoading:
                    return APIRouteListStatus.InLoading;
                case RouteListStatus.EnRoute:
                    return APIRouteListStatus.EnRoute;
                case RouteListStatus.Delivered:
                    return APIRouteListStatus.Delivered;
                case RouteListStatus.OnClosing:
                    return APIRouteListStatus.OnClosing;
                case RouteListStatus.MileageCheck:
                    return APIRouteListStatus.MileageCheck;
                case RouteListStatus.Closed:
                    return APIRouteListStatus.Closed;
                default:
                    return APIRouteListStatus.Unknown;
            }
        }
    
        private APIRouteListAddressStatus convertToAPIRouteListAddressStatus(RouteListItemStatus routeListItemStatus)
        {
            switch (routeListItemStatus)
            {
                case RouteListItemStatus.EnRoute:
                    return APIRouteListAddressStatus.EnRoute;
                case RouteListItemStatus.Completed:
                    return APIRouteListAddressStatus.Completed;
                case RouteListItemStatus.Canceled:
                    return APIRouteListAddressStatus.Canceled;
                case RouteListItemStatus.Overdue:
                    return APIRouteListAddressStatus.Overdue;
                case RouteListItemStatus.Transfered:
                    return APIRouteListAddressStatus.Transfered;
                default:
                    return APIRouteListAddressStatus.Unknown;
            }
        }

        private APIRouteListAddress convertToAPIRouteListAddress(RouteListItem routeListAddress)
        {
            return new APIRouteListAddress()
            {
                Id = routeListAddress.Id,
                Status = convertToAPIRouteListAddressStatus(routeListAddress.Status),
                DeliveryTime = routeListAddress.Order.DeliveryDate ?? DateTime.MinValue,
                OrderId = routeListAddress.Order.Id,
                FullBottlesCount = routeListAddress.Order.BottlesReturn ?? 0,
                Address = extractAddressFromDeliveryPoint(routeListAddress.Order.DeliveryPoint)
            };
        }

        private APIAddress extractAddressFromDeliveryPoint(DeliveryPoint deliveryPoint)
        {
            return new APIAddress()
            {
                City = deliveryPoint.City,
                Street = deliveryPoint.Street,
                Building = deliveryPoint.Building + deliveryPoint.Letter,
                Entrance = deliveryPoint.Entrance,
                Floor = deliveryPoint.Floor,
                Apartment = deliveryPoint.Room
            };
        }
    }
}
