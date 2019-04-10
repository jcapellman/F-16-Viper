using F16Viper.library.GameStates.Base;
using F16Viper.library.Renderables;

using Microsoft.Xna.Framework.Content;

namespace F16Viper.library.GameStates
{
    public class MainGameState : BaseGameState
    {
        public override void LoadContent(ContentManager contentManager)
        {
            var airplane = new Airplane("F18");
            airplane.LoadContent(contentManager);

            AddRenderable(airplane);
        }
    }
}