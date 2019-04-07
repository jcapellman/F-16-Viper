using System.Collections.Generic;

using F16Viper.library.Renderables.Base;

using Microsoft.Xna.Framework.Graphics;

namespace F16Viper.library.GameStates.Base
{
    public abstract class BaseGameState
    {
        private List<BaseRenderable> _renderables = new List<BaseRenderable>();

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var renderable in _renderables)
            {
                renderable.Render(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}