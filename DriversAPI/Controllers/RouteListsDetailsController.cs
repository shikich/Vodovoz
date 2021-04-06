using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            this.aPIRouteListData = aPIRouteListData ?? throw new ArgumentNullException(nameof(aPIRouteListData));
        }

        // GET: RouteListsDetails 
        public IEnumerable<APIRouteList> Get([FromBody] int[] routeListsIds)
        {
            return aPIRouteListData.Get(routeListsIds);
        }
    }
}
