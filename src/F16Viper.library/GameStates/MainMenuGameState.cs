using F16Viper.library.GameStates.Base;
using F16Viper.library.Renderables;

using Microsoft.Xna.Framework.Content;

namespace F16Viper.library.GameStates
{
    public class MainMenuGameState : BaseGameState
    {
        private const string BackgroundTextureName = "MenuBG";
        
        public override void LoadContent(ContentManager contentManager)
        {
            var background = new FSSplash(BackgroundTextureName);
            background.LoadContent(contentManager);

            AddRenderable(background);
        }
    }
}