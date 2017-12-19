using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LanguageBuilder.Web.Hubs
{
    public class WordsHub : Hub
    {
        //public Task Send(string message)
        //{
        //    return Clients.All.InvokeAsync("Send", message);
        //}

        public override Task OnConnectedAsync()
        {
            Clients.All.InvokeAsync("broadcastMessage", $"{Context.ConnectionId} joined the conversation");
            return base.OnConnectedAsync();
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.InvokeAsync("broadcastMessage", name, message);
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.InvokeAsync("broadcastMessage", "system", $"{Context.ConnectionId} left the conversation");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
