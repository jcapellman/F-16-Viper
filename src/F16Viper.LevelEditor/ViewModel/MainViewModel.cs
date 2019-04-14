using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using F16Viper.Common.library.JSONObjects;
using F16Viper.Common.library.Managers;

namespace F16Viper.LevelEditor.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentFileName = string.Empty;
        private LevelJSON _currentLevel;

        private ObservableCollection<string> _tiles;

        public ObservableCollection<string> Tiles
        {
            get => _tiles;

            set
            {
                _tiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _currentMapTiles;

        public ObservableCollection<string> CurrentMapTiles
        {
            get => _currentMapTiles;

            set
            {
                _currentMapTiles = value;
                OnPropertyChanged();
            }
        }

        private string _selectedTile;

        public string SelectedTile
        {
            get => _selectedTile;

            set
            {
                _selectedTile = value;
                OnPropertyChanged();
            }
        }

        // TODO: Make this configurable
        private const string TILE_FILE_PATH = "../../../../../assets/gfx/Tiles/";

        public MainViewModel()
        {
            LoadTiles();

            NewLevel();
        }

        private void LoadTiles()
        {
            Tiles = new ObservableCollection<String>();

            var files = Directory.GetFiles(TILE_FILE_PATH);

            foreach (var file in files)
            {
                Tiles.Add(new FileInfo(file).FullName);
            }

            SelectedTile = Tiles.FirstOrDefault();
        }

        internal void AddTile()
        {
            CurrentMapTiles.Add(SelectedTile);
        }

        internal void NewLevel()
        {
            _currentFileName = string.Empty;

            _currentLevel = new LevelJSON();

            CurrentMapTiles = new ObservableCollection<string>();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}