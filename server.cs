using Microsoft.AspNet.SignalR;
using serverhost.MongoEntities;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace serverhost.Hubs
{
    public class MainHub : Hub
    {

        public void SendMessage(string message)
        {
            Clients.All.newMessage(string.Format("{0} : {1}", this.Context.ConnectionId, message)); // отправляет данные всем подключённым клиентам
        }

    }
      
}