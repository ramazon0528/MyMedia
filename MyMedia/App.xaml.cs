using System.Configuration;
using System.Data;
using System.Windows;
using MyMedia.Helpers;

namespace MyMedia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DI.Init();

            var mainWindow = DI.GetRequiredService<MainWindow>();

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
