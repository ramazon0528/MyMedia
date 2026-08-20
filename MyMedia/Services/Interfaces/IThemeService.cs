namespace MyMedia.Services.Interfaces;

public interface IThemeService
{
    void Initialize();
    void SetTheme(string themeName);
    void SaveTheme(string themeName);
    IReadOnlyCollection<string> GetThemes();
    string CurrentTheme { get; }
}
