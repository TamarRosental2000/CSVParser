using Newtonsoft.Json;
using System.Threading;

namespace CSVParser.Cache.Logic
{
    public class CacheLogic
    {
        private static readonly object fileLock = new object();
        static void SaveToCache(PlayerModel playerData)
        {
            string cachePath = GetCacheFilePath(playerData.Id);
            string json = JsonConvert.SerializeObject(playerData);

            lock (fileLock)
            {
                File.WriteAllText(cachePath, json);
            }
        }

        static PlayerModel LoadFromCache(int playerId)
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

        static string GetCacheFilePath(int playerId)
        {
            // Generate cache file path based on player ID
            string cacheFolder = "cache"; // Replace with your desired cache folder
            string cacheFileName = $"player_{playerId}.json";
            return Path.Combine(cacheFolder, cacheFileName);
        }
    }

    class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    In this code, the FileLock object is used as a synchronization primitive to ensure that only one thread can access the critical section (read or write) at a time. The lock statement is used to acquire the lock before performing file read or write operations and release it afterward.

Please keep in mind that while this approach provides basic thread safety, it's important to consider potential performance implications, especially in scenarios with high concurrency. For more advanced synchronization and thread safety, you might explore other mechanisms such as SemaphoreSlim, Mutex, or higher-level constructs provided by concurrent programming libraries.






    }
}
