using System.Windows;
using MyMedia.Services.Interfaces;

namespace MyMedia.Services;

public class WindowService : IWindowService
{
    private Window MainWindow => Application.Current.MainWindow;

    public void Close() => MainWindow.Close();

    public void Minimize() => MainWindow.WindowState = WindowState.Minimized;

    public void Maximize()
    {
        MainWindow.WindowState =
            MainWindow.WindowState == WindowState.Normal
                ? WindowState.Maximized
                : WindowState.Normal;
    }
}
