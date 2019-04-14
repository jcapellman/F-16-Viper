using System.Collections.Generic;

namespace F16Viper.Common.library.JSONObjects
{
    public class LevelJSON
    {
        public List<string> Textures { get; set; }

        public List<int> Tiles { get; set; }

        public LevelJSON()
        {
            Textures = new List<string>();
            Tiles = new List<int>();
        }
    }
}