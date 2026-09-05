using System.CodeDom;
using System.Collections.ObjectModel;
using MyMedia.Commands;
using MyMedia.Services.Interfaces;

namespace MyMedia.ViewModels.Pages;

public class SettingsViewModel : ViewModelBase
{
    private readonly IThemeService _themeService;

    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;

        Themes = new(_themeService.GetThemes());

        SaveSettingsCommand = new(SaveSettings);
    }

    private void SaveSettings()
    {
        if (SelectedTheme == null)
            return;

        _themeService.SaveTheme(SelectedTheme);
    }

    public ObservableCollection<string> Themes { get; } = [];

    private string? _selectedTheme;
    public string? SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if (_selectedTheme == value)
                return;

            _selectedTheme = value;
            OnPropertyChanged();

            if (_selectedTheme != null)
                _themeService.SetTheme(_selectedTheme);
        }
    }

    public RelayCommand SaveSettingsCommand { get; }
}
