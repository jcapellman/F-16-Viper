using System;
using System.Collections.Generic;

using F16Viper.library.Containers;
using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F16Viper.library.GameStates.Base
{
    public abstract class BaseGameState
    {
        public event EventHandler<Type> OnStateChange;

        protected KeyboardState _oldKeyboardState;

        protected void ChangeState(Type type)
        {
            OnStateChange?.Invoke(null, type);
        }

        private List<BaseRenderable> _renderables = new List<BaseRenderable>();

        protected void AddRenderable(BaseRenderable renderable)
        {
            _renderables.Add(renderable);
        }

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void Render(SpriteBatch spriteBatch, WindowContainer windowContainer)
        {
            spriteBatch.Begin();

            foreach (var renderable in _renderables)
            {
                renderable.Render(spriteBatch, windowContainer);
            }

            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState)
        {
            foreach (var renderable in _renderables)
            {
                renderable.Update(gameTime, keyboardState, gamePadState);
            }
        }
    }
}