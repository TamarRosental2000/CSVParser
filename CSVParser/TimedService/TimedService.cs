using CSVParser.Logic;

namespace CSVParser.TimedService
{
    public class TimedService : BackgroundService
    {
        public PlayerLogic _playerLogic;
        public TimedService(PlayerLogic playerLogic)
        {
            _playerLogic = playerLogic;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _playerLogic.UpdatePlayerData();
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
