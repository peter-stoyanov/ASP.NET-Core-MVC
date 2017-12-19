using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LanguageBuilder.Web.Hubs
{
    public class NotificationsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.InvokeAsync("onConnected", "SignalR Connected");

            return base.OnConnectedAsync();
        }

        public void Broadcast(string connectionId, string message)
        {
            Clients.Client(connectionId).InvokeAsync("broadcastMessage", message);
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.InvokeAsync("onDisconnected", "SignalR disconnected");

            return base.OnDisconnectedAsync(exception);
        }
    }
}
