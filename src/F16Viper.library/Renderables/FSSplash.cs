using F16Viper.library.Containers;
using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.Renderables
{
    public class FSSplash : BaseRenderable
    {
        private readonly string _textureName;

        private Texture2D _texture;

        public FSSplash(string textureName)
        {
            _textureName = textureName;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>($"gfx/{_textureName}");
        }

        public override void Render(SpriteBatch spriteBatch, WindowContainer windowContainer)
        {
            spriteBatch.Draw(_texture, Vector2.One, new Rectangle(0, 0, windowContainer.ResolutionX, windowContainer.ResolutionY), Color.White);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            
        }
    }
}