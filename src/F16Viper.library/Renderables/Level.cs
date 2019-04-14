using System.Collections.Generic;
using System.IO;
using System.Linq;

using F16Viper.Common.library.Managers;

using F16Viper.library.Containers;
using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.Renderables
{
    public class Level : BaseRenderable
    {
        private readonly string _levelName;

        private readonly Dictionary<int, Texture2D> _textureMaps = new Dictionary<int, Texture2D>();

        public class RenderableTile
        {
            public int TextureID { get; set; }

            public Vector2 Position { get; set; }
        }

        private List<RenderableTile> _tiles = new List<RenderableTile>();

        public Level(string levelName)
        {
            _levelName = levelName;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            var level = LevelManager.LoadLevel(_levelName);

            var index = 0;

            foreach (var texture in level.Textures.Select(Path.GetFileNameWithoutExtension))
            {
                _textureMaps.Add(index, contentManager.Load<Texture2D>($"Tiles/{texture}"));

                index++;
            }

            index = 0;

            foreach (var tile in level.Tiles)
            {
                _tiles.Add(new RenderableTile
                {
                    Position = new Vector2(0, _textureMaps[tile].Height * index * -1),
                    TextureID = tile
                });

                index++;
            }
        }

        public override void Render(SpriteBatch spriteBatch, WindowContainer windowContainer)
        {
            foreach (var tile in _tiles)
            {
                spriteBatch.Draw(_textureMaps[tile.TextureID],
                    tile.Position,
                    new Rectangle(0, 0, _textureMaps[tile.TextureID].Width, _textureMaps[tile.TextureID].Height),
                    Color.White);
            }
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            foreach (var tile in _tiles)
            {
                var tilePosition = tile.Position;
                tilePosition.Y += 2;

                tile.Position = tilePosition;
            }
        }
    }
}