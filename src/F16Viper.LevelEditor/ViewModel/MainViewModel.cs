using System.ComponentModel;
using System.Windows.Forms;

using F16Viper.Common.library.JSONObjects;
using F16Viper.Common.library.Managers;

namespace F16Viper.LevelEditor.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _currentFileName = string.Empty;
        private LevelJSON _currentLevel;

        public MainViewModel()
        {
            _currentLevel = new LevelJSON();
        }

        internal void NewLevel()
        {
            _currentFileName = string.Empty;

            _currentLevel = new LevelJSON();
        }

        internal void LoadLevel()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "level files (*.map)|*.map";

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _currentFileName = ofd.FileName;

                _currentLevel = LevelManager.LoadLevelFile(_currentFileName);
            }
        }

        internal void SaveLevel()
        {
            if (string.IsNullOrEmpty(_currentFileName))
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "level files (*.map)|*.map";

                    if (sfd.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    _currentFileName = sfd.FileName;
                }
            }

            LevelManager.SaveLevel(_currentFileName, _currentLevel);
        }
    }
}