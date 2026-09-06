using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMedia.AppLayer.Configuration;
using MyMedia.AppLayer.Services;
using MyMedia.Infrastructure;
using MyMedia.Infrastructure.Data;
using MyMedia.Infrastructure.Services;
using MyMedia.Services;
using MyMedia.Services.Interfaces;
using MyMedia.ViewModels.Pages;
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
        services.AddTransient<AddMediaViewModel>();
        services.AddTransient<MediaViewModel>();
        services.AddTransient<CategoryViewModel>();
        services.AddTransient<GenreViewModel>();
        services.AddTransient<SettingsViewModel>();

        services.AddTransient<MediaService>();
        services.AddTransient<CategoryService>();
        services.AddTransient<GenreService>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.Configure<ThemeOptions>(configuration.GetSection("Theme"));

        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IImageService, ImageService>();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<NavigationService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public static void MigrateDatabase()
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }

    public static T GetRequiredService<T>()
        where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
