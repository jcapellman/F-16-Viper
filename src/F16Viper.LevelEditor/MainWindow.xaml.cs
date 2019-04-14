using System.Windows;

using F16Viper.LevelEditor.ViewModel;

namespace F16Viper.LevelEditor
{
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

        }

        private void MnuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MnuItemNew_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewLevel();
        }

        private void MnuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadLevel();
        }

        private void MnuItemSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveLevel();
        }

        private void BtnAddTile_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTile();

            lvMap.SelectedIndex = lvMap.Items.Count - 1;
            lvMap.ScrollIntoView(lvMap.SelectedItem);
        }
    }
}