using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMedia.AppLayer.Configuration;
using MyMedia.AppLayer.Services;
using MyMedia.Infrastructure;
using MyMedia.Infrastructure.Services;
using MyMedia.ViewModels.Windows;

namespace MyMedia.Helpers;

public class DI
{
    private static IServiceProvider _serviceProvider = null!;

    public static void Init()
    {
        var services = new ServiceCollection();

        services.AddInfrastructure($"Data Source={DbPathProvider.GetDbPath()}");

        services.AddTransient<MainWindow>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MediaService>();
        services.AddTransient<CategoryService>();
        services.AddTransient<GenreService>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.Configure<ThemeOptions>(configuration.GetSection("Theme"));

        services.AddSingleton<IThemeService, ThemeService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public static T GetRequiredService<T>()
        where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
