using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleRDemo.Hubs
{

    [HubName("productHub")]
    public class ProductHub : Hub
    {

        [HubMethodName("announce")]
        public void Announce(string message)
        {
          //  Clients.All.Announce(message);
            Clients.All.Announce(message);

        }

        [HubMethodName("displayProducts")]
        public void Show() {

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ProductHub>();
            context.Clients.All.displayProducts();

        }

    }
}