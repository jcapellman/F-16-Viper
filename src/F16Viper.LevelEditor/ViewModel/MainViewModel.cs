using System;
using System.Collections.Generic;
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
        public struct Tile
        {
            public string FilePath { get; set; }

            public Guid ID { get; set; }

            public Tile(string filePath)
            {
                FilePath = filePath;
                ID = Guid.NewGuid();
            }
        }

        private string _currentFileName = string.Empty;
        private LevelJSON _currentLevel;

        private Dictionary<string, string> TileMapping = new Dictionary<string, string>();

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

        private ObservableCollection<Tile> _currentMapTiles;

        public ObservableCollection<Tile> CurrentMapTiles
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
                var fileInfo = new FileInfo(file);

                TileMapping.Add(fileInfo.Name, fileInfo.FullName);

                Tiles.Add(fileInfo.FullName);
            }

            SelectedTile = Tiles.FirstOrDefault();
        }

        internal void AddTile()
        {
            CurrentMapTiles.Add(new Tile(SelectedTile));
        }

        internal void NewLevel()
        {
            _currentFileName = string.Empty;

            _currentLevel = new LevelJSON();

            CurrentMapTiles = new ObservableCollection<Tile>();
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

                foreach (var tile in _currentLevel.Tiles)
                {
                    var textureName = _currentLevel.Textures[tile];

                    CurrentMapTiles.Add(new Tile(TileMapping[textureName]));
                }
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

            _currentLevel = new LevelJSON();

            foreach (var tile in CurrentMapTiles)
            {
                var tileNameOnly = TileMapping.FirstOrDefault(a => a.Value == tile.FilePath).Key;

                var textureIndex = _currentLevel.Textures.IndexOf(tileNameOnly);

                if (textureIndex == -1)
                {
                    _currentLevel.Textures.Add(tileNameOnly);

                    textureIndex = _currentLevel.Textures.Count - 1;
                }

                _currentLevel.Tiles.Add(textureIndex);
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