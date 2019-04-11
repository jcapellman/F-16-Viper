﻿using F16Viper.library.GameStates.Base;
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
            var player = new Player();
            player.LoadContent(contentManager);

            AddRenderable(player);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // TODO: Move Player Left
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                // TODO: Move Player Right
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                // TODO: Move Player Up
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                // TODO: Move Player Down
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