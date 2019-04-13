using F16Viper.library.Containers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.Renderables.Base
{
    public abstract class BaseRenderable
    {
        protected Vector2 Position;

        public void UpdatePosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public abstract void LoadContent(ContentManager contentManager);

        public abstract void Render(SpriteBatch spriteBatch, WindowContainer windowContainer);

        public abstract void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState);
    }
}