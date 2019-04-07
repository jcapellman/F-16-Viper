﻿using System;
using System.Collections.Generic;

using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace F16Viper.library.GameStates.Base
{
    public abstract class BaseGameState
    {
        public event EventHandler<Type> OnStateChange;

        protected void ChangeState(Type type)
        {
            OnStateChange?.Invoke(null, type);
        }

        private List<BaseRenderable> _renderables = new List<BaseRenderable>();

        public virtual void LoadContent(ContentManager contentManager)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var renderable in _renderables)
            {
                renderable.Render(spriteBatch);
            }

            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var renderable in _renderables)
            {
                renderable.Update(gameTime);
            }
        }
    }
}