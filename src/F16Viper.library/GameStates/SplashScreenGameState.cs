using System;

using F16Viper.library.GameStates.Base;
using F16Viper.library.Renderables;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.GameStates
{
    public class SplashScreenGameState : BaseGameState
    {
        private const string BackgroundTextureName = "SplashBG";

        public override void LoadContent(ContentManager contentManager)
        {
            try
            {
                var background = new FSSplash(BackgroundTextureName);
                background.LoadContent(contentManager);

                AddRenderable(background);
            }
            catch (Exception)
            {
                // Allow the app to start even when splash content is missing.
            }
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            if (_oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
            {
                ChangeState(typeof(MainGameState));
            }

            base.Update(gameTime, keyboardState, gamePadState);
        }
    }
}
