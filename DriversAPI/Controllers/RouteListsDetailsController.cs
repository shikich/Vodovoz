using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Library.DataAccess;
using WebAPI.Library.Models;

namespace DriversAPI.Controllers
{
    public class RouteListsDetailsController : ApiController
    {
        private readonly APIRouteListData aPIRouteListData;

        public RouteListsDetailsController(APIRouteListData aPIRouteListData)
        {
            this.aPIRouteListData = aPIRouteListData;
        }

        public IEnumerable<APIRouteList> Get(int[] routeListsIds)
        {
            return aPIRouteListData.Get(routeListsIds);
        }
    }
}