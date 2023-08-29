using CSVParser.Model;
using Newtonsoft.Json;
using System.Threading;

namespace CSVParser.Cache.Logic
{
    public static class CacheLogic
    {
        private static readonly object fileLock = new object();
        public static void SaveToCache(PlayerModel playerData)
        {
            string cachePath = GetCacheFilePath(playerData.Id);
            string json = JsonConvert.SerializeObject(playerData);

            lock (fileLock)
            {
                File.WriteAllText(cachePath, json);
            }
        }

        public static PlayerModel LoadFromCache(int playerId)
        {
            string cachePath = GetCacheFilePath(playerId);

            lock (fileLock)
            {
                if (File.Exists(cachePath))
                {
                    string json = File.ReadAllText(cachePath);
                    return JsonConvert.DeserializeObject<PlayerModel>(json);
                }
            }
            return null;
        }

        public static string GetCacheFilePath(int playerId)
        {
            // Generate cache file path based on player ID
            string cacheFolder = "cache"; // Replace with your desired cache folder
            string cacheFileName = $"player_{playerId}.json";
            return Path.Combine(cacheFolder, cacheFileName);
        }
    }

}
