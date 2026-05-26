using System.IO;
using System.Text.Json;

using F16Viper.Common.library.JSONObjects;

namespace F16Viper.Common.library.Managers
{
    public class LevelManager
    {
        public static LevelJSON LoadLevelFile(string fileName) => JsonSerializer.Deserialize<LevelJSON>(File.ReadAllText(fileName));

        public static LevelJSON LoadLevel(string levelName) => LoadLevelFile($"Levels/{levelName}.map");

        public static bool SaveLevel(string fileName, LevelJSON level)
        {
            var json = JsonSerializer.Serialize(level);

            File.WriteAllText(fileName, json);

            return true;
        }
    }
}
