using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LanguageBuilder.Web.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        private static HashSet<string> _backgroundTasks = new HashSet<string>();

        public static void AddBackgroundTaskForUser(string username)
        {
            _backgroundTasks.Add(username);
        }

        public static void RemoveBackgroundTaskForUser(string username)
        {
            _backgroundTasks.Remove(username);
        }

        public override Task OnConnectedAsync()
        {
            Clients.All.InvokeAsync("onConnected", "SignalR Connected");

            string name = Context.User.Identity.Name;

            Groups.AddAsync(Context.ConnectionId, name);

            this.HasBackgroundTask();

            return base.OnConnectedAsync();
        }

        public void HasBackgroundTask()
        {
            string name = Context.User.Identity.Name;

            if (_backgroundTasks.Contains(name))
            {
                Clients.Group(name)?.InvokeAsync("initTaskNotification", "initTaskNotification");
            }
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
