using System.Linq;

using F16Viper.library.GameStates.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.GameStates
{
    public class SplashScreenGameState : BaseGameState
    {
        public override void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            if (keyboardState.GetPressedKeys().Any())
            {
                ChangeState(typeof(MainMenuGameState));
            }

            base.Update(gameTime, keyboardState, gamePadState);
        }
    }
}