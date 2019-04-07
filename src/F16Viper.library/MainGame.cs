using System;

using F16Viper.library.GameStates;
using F16Viper.library.GameStates.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace F16Viper
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private BaseGameState _currentGameState;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ChangeState(typeof(SplashScreenGameState));
        }

        private void ChangeState(Type typeOfState)
        {
            var state = (BaseGameState) Activator.CreateInstance(typeOfState);

            state.OnStateChange -= _currentGameState_OnStateChange;
            state.OnStateChange += _currentGameState_OnStateChange;
        }

        private void _currentGameState_OnStateChange(object sender, Type e)
        {
            ChangeState(e);
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentGameState.Render(spriteBatch);

            base.Draw(gameTime);
        }
    }
}