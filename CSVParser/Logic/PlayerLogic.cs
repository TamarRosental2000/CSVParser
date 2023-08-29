using CsvHelper.Configuration;
using CsvHelper;
using CSVParser.Model;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using CSVParser.Cache.Logic;

namespace CSVParser.Logic
{
    public class PlayerLogic
    {
        public const string CSV_FILE_PATH_INPUT = ".\\File\\players.csv";
        public const string CSV_FILE_PATH_Output = ".\\File\\playersData.csv";
        private readonly ReadCSVFile _readCSVFile;
        private readonly HttpClient _httpClient;

        public PlayerLogic(HttpClient httpClient, ReadCSVFile readCSVFile)
        {
            _httpClient = httpClient;
            _readCSVFile = readCSVFile;
        }
        public async Task<List<PlayerModel>> GetPlayerData()
        {
            var csvPlayers = _readCSVFile.ReadRecords<PlayerCSVModel>(CSV_FILE_PATH_INPUT);
            var players = new List<PlayerModel>();
            foreach (var player in csvPlayers)
            {
                var playerModel = await GetPlayerInfoFromApi(player.id);
                players.Add(playerModel);
            }

            CreateCSVFile(players);
            
            return players;
        }
        private async Task<PlayerModel> GetPlayerInfoFromApi(int playerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://www.balldontlie.io/api/v1/players/{playerId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var playerModel = JsonConvert.DeserializeObject<PlayerModel>(json);
                CacheLogic.SaveToCache(playerModel);
                return playerModel;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void CreateCSVFile(IEnumerable<PlayerModel> playerModels)
        {
            var downloadPath = GetDownloadDirectoryPath();
            using (var writer = new StreamWriter(downloadPath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(playerModels);
            }
        }
        static string GetDownloadDirectoryPath()
        {
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downloadPath = Path.Combine(downloadPath, "Downloads\\playersFullData.csv");

            return downloadPath;
        }
    }

}
