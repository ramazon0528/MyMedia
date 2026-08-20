using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;
using Microsoft.Extensions.Options;
using MyMedia.AppLayer.Configuration;
using MyMedia.Services.Interfaces;

namespace MyMedia.Infrastructure.Services;

public class ThemeService : IThemeService
{
    private readonly ThemeOptions _options;
    private ResourceDictionary? _currentTheme;
    private readonly string _settingsPath;

    public string CurrentTheme => _options.Current;

    public ThemeService(IOptions<ThemeOptions> themeOptions)
    {
        _options = themeOptions.Value;
        _settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    }

    public void Initialize()
    {
        SetTheme(_options.Current);
    }

    public void SaveTheme(string themeName)
    {
        SetTheme(themeName);

        var json = File.ReadAllText(_settingsPath);

        var root =
            JsonNode.Parse(json)
            ?? throw new InvalidOperationException("Could not parse appsettings.json.");

        var themeSection =
            root["Theme"]
            ?? throw new InvalidOperationException(
                "Theme section was not found in appsettings.json."
            );

        themeSection["Current"] = themeName;

        var options = new JsonSerializerOptions { WriteIndented = true };

        File.WriteAllText(_settingsPath, root.ToJsonString(options));
    }

    public void SetTheme(string themeName)
    {
        if (!_options.Themes.TryGetValue(themeName, out var theme))
            throw new InvalidOperationException($"Theme '{themeName}' was not found.");

        var dictionary = new ResourceDictionary
        {
            Source = new Uri(theme.Source, UriKind.Relative),
        };

        var resources = Application.Current.Resources;

        if (_currentTheme != null)
            resources.MergedDictionaries.Remove(_currentTheme);

        resources.MergedDictionaries.Add(dictionary);

        _currentTheme = dictionary;
    }

    public IReadOnlyCollection<string> GetThemes() => _options.Themes.Keys;
}
