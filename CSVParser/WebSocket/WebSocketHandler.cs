using CSVParser.Model;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
namespace CSVParser.WebSocketHandler
{
    public class WebSocketHandler
    {
        private readonly List<WebSocket> _sockets = new List<WebSocket>();
    
        public async Task AddSocket(WebSocket socket)
        {
            _sockets.Add(socket);
        }
    
        public async Task RemoveSocket(WebSocket socket)
        {
            _sockets.Remove(socket);
        }
    
        public async Task SendPlayerListToClients(IEnumerable<PlayerModel> playerList)
        {
            var serializedPlayerList = JsonConvert.SerializeObject(playerList);
            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedPlayerList));
    
            foreach (var socket in _sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

    }

}
