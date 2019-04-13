using F16Viper.library.Containers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.Renderables
{
    public class Player : Airplane
    {
        private const string PLAYER_STARTING_PLANE = "F18";

        // TODO: Have these pulled per plane type
        public const int MOVEMENT_Y = 10;
        public const int MOVEMENT_X = 10;

        public Player() : base(PLAYER_STARTING_PLANE) { }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public override void Render(SpriteBatch spriteBatch, WindowContainer windowContainer)
        {
            base.Render(spriteBatch, windowContainer);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            base.Update(gameTime, keyboardState, gamePadState);
        }
    }
}