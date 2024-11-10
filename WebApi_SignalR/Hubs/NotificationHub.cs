using Microsoft.AspNetCore.SignalR;

namespace WebApi_SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
