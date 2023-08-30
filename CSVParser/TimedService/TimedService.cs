using CSVParser.Logic;
using CSVParser.WebSocket;

namespace CSVParser.TimedService
{
    public class TimedService : BackgroundService
    {
        public PlayerLogic _playerLogic;
        public NotificationHub _notificationHub;
        public TimedService(PlayerLogic playerLogic, NotificationHub notificationHub)
        {
            _playerLogic = playerLogic;
            _notificationHub = notificationHub;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _playerLogic.UpdatePlayerData();
                if (response.Item1)
                {
                    _notificationHub.SendNotification("Player updated");
                }
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
