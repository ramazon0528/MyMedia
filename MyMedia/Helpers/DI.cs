using Microsoft.Extensions.DependencyInjection;
using MyMedia.AppLayer.Services;
using MyMedia.Infrastructure;
using MyMedia.ViewModels.Windows;

namespace MyMedia.Helpers;

public class DI
{
    private static IServiceProvider _serviceProvider;

    public static void Init()
    {
        var services = new ServiceCollection();

        services.AddInfrastructure(DbPathProvider.GetDbPath());

        services.AddTransient<MainWindow>();
        services.AddTransient<MainViewModel>();

        services.AddTransient<MediaService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public static T GetRequiredService<T>()
        where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
