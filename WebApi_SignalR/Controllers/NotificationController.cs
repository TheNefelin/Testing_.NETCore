using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi_SignalR.Hubs;

namespace WebApi_SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageToAll(string msge)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", msge);
            return Ok("Mensaje enviado a todos los clientes.");
        }
    }
}
