using System;

using F16Viper.library.GameStates;
using F16Viper.library.GameStates.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private BaseGameState _currentGameState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ChangeState(typeof(SplashScreenGameState));
        }

        private void ChangeState(Type typeOfState)
        {
            var state = (BaseGameState) Activator.CreateInstance(typeOfState);

            state.OnStateChange -= _currentGameState_OnStateChange;
            state.OnStateChange += _currentGameState_OnStateChange;

            state.LoadContent(Content);

            _currentGameState = state;
        }

        private void _currentGameState_OnStateChange(object sender, Type e)
        {
            ChangeState(e);
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.Update(gameTime, Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentGameState.Render(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}