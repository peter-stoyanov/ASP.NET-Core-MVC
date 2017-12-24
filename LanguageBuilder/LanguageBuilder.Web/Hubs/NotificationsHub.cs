using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Web.Http;

namespace LanguageBuilder.Web.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.InvokeAsync("onConnected", "SignalR Connected");

            string name = Context.User.Identity.Name;

            Groups.AddAsync(Context.ConnectionId, name);

            return base.OnConnectedAsync();
        }

        public void Send(string who, string message)
        {
            Clients.Client(who)?.InvokeAsync("broadcastMessage", message);
        }

        public void Broadcast(string connectionId, string message)
        {
            Clients.All.InvokeAsync("broadcastMessage", message);
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.InvokeAsync("onDisconnected", "SignalR disconnected");

            string name = Context.User.Identity.Name;

            Groups.RemoveAsync(Context.ConnectionId, name);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
