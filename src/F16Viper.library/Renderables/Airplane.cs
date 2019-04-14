using F16Viper.library.Containers;
using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.Renderables
{
    public class Airplane : BaseRenderable
    {
        private readonly string _airplane;

        private Texture2D _airplaneTexture;

        public Airplane(string airplaneName)
        {
            _airplane = airplaneName;

            Position = Vector2.One;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _airplaneTexture = contentManager.Load<Texture2D>($"fighters/{_airplane}");
        }

        public override void Render(SpriteBatch spriteBatch, WindowContainer windowContainer)
        {
            spriteBatch.Draw(_airplaneTexture, Position, Color.White);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {

        }
    }
}