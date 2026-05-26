using System;

using F16Viper.library.GameStates.Base;
using F16Viper.library.Renderables;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.GameStates
{
    public class MainGameState : BaseGameState
    {
        public override void LoadContent(ContentManager contentManager)
        {
            try
            {
                var level = new Level("E1M1");
                level.LoadContent(contentManager);

                AddRenderable(level);
            }
            catch (Exception)
            {
                // Allow the game to start without level content.
            }

            try
            {
                var player = new Player();
                player.LoadContent(contentManager);

                AddRenderable(player);
            }
            catch (Exception)
            {
                // Allow the game to start without player content.
            }
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            var player = GetRenderable<Player>();

            if (player != null)
            {
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    player.UpdatePosition(-Player.MOVEMENT_X, 0);
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    player.UpdatePosition(Player.MOVEMENT_X, 0);
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    player.UpdatePosition(0, -Player.MOVEMENT_Y);
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    player.UpdatePosition(0, Player.MOVEMENT_Y);
                }
            }

            if (_oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
            {
                // TODO: Add Fire
            }

            _oldKeyboardState = keyboardState;

            base.Update(gameTime, keyboardState, gamePadState);
        }
    }
}
