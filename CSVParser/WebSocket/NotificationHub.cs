using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace CSVParser.WebSocket
{
    public class NotificationHub:Hub
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendNotification(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
