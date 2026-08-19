using System.Configuration;
using System.Data;
using System.Windows;
using MyMedia.AppLayer.Services;
using MyMedia.Helpers;
using MyMedia.Infrastructure.Services;

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

            var themeService = DI.GetRequiredService<IThemeService>();

            themeService.Initialize();

            var mainWindow = DI.GetRequiredService<MainWindow>();

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
