using System.IO;

using F16Viper.Common.library.JSONObjects;

using Newtonsoft.Json;

namespace F16Viper.Common.library.Managers
{
    public class LevelManager
    {
        public static LevelJSON LoadLevelFile(string fileName) => JsonConvert.DeserializeObject<LevelJSON>(File.ReadAllText(fileName));

        public static LevelJSON LoadLevel(string levelName) => LoadLevelFile($"Levels/{levelName}.map");

        public static bool SaveLevel(string fileName, LevelJSON level)
        {
            var json = JsonConvert.SerializeObject(level);

            File.WriteAllText(fileName, json);

            return true;
        }
    }
}