using System.IO;

using F16Viper.Common.library.JSONObjects;

using Newtonsoft.Json;

namespace F16Viper.Common.library.Managers
{
    public class LevelManager
    {
        public static LevelJSON LoadLevel(string levelName) => JsonConvert.DeserializeObject<LevelJSON>(File.ReadAllText($"Levels/{levelName}.json"));
    }
}