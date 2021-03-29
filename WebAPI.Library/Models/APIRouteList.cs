using System.Collections.Generic;

namespace WebAPI.Library.Models
{
    public class APIRouteList
    {
        public int Id { get; set; }
        public APIRouteListStatus Status { get; set; }
        public IList<APIRouteListAddress> RouteListAddresses { get; set; }
    }
}
