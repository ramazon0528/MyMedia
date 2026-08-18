using System.Windows;
using MyMedia.ViewModels.Windows;

namespace MyMedia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            InitializeComponent();

            DataContext = _mainViewModel;
        }
    }
}
