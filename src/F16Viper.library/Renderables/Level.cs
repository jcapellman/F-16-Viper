using System.Collections.Generic;
using System.IO;

using F16Viper.library.Containers;
using F16Viper.library.JSONObjects;
using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Newtonsoft.Json;

namespace F16Viper.library.Renderables
{
    public class Level : BaseRenderable
    {
        private readonly string _levelName;

        private readonly List<Texture2D> _textures = new List<Texture2D>();

        public Level(string levelName)
        {
            _levelName = levelName;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            var levelObject = JsonConvert.DeserializeObject<LevelJSON>(File.ReadAllText($"Levels/{_levelName}.json"));

            foreach (var texture in levelObject.Textures)
            {
                _textures.Add(contentManager.Load<Texture2D>($"Tiles/{texture}"));
            }
        }

        public override void Render(SpriteBatch spriteBatch, WindowContainer windowContainer)
        {

        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
        }
    }
}