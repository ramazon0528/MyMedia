using MyMedia.Commands;
using MyMedia.Services;
using MyMedia.Services.Interfaces;
using MyMedia.ViewModels.Pages;

namespace MyMedia.ViewModels.Windows;

public class MainViewModel : ViewModelBase
{
    private readonly IWindowService _windowService;

    public NavigationService Navigator { get; set; }

    private readonly MediaViewModel _mediaViewModel;
    private readonly CategoryViewModel _categoryViewModel;
    private readonly GenreViewModel _genreViewModel;
    private readonly SettingsViewModel _settingsViewModel;

    public MainViewModel(
        IWindowService windowService,
        NavigationService navigationService,
        MediaViewModel mediaViewModel,
        CategoryViewModel categoryViewModel,
        GenreViewModel genreViewModel,
        SettingsViewModel settingsViewModel
    )
    {
        _windowService = windowService;
        _mediaViewModel = mediaViewModel;
        _categoryViewModel = categoryViewModel;
        _genreViewModel = genreViewModel;
        _settingsViewModel = settingsViewModel;

        Navigator = navigationService;

        Navigator.NavigateTo(_mediaViewModel);

        CloseCommand = new(() => _windowService.Close());
        MaximizeCommand = new(() => _windowService.Maximize());
        MinimizeCommand = new(() => _windowService.Minimize());

        NavigateToMediaPageCommand = new(() => Navigator.NavigateTo(_mediaViewModel));
        NavigateToCategoryPageCommand = new(() => Navigator.NavigateTo(_categoryViewModel));
        NavigateToGenrePageCommand = new(() => Navigator.NavigateTo(_genreViewModel));
        NavigateToSettingsPageCommand = new(() => Navigator.NavigateTo(_settingsViewModel));
    }

    public RelayCommand CloseCommand { get; }
    public RelayCommand MaximizeCommand { get; }
    public RelayCommand MinimizeCommand { get; }
    public RelayCommand NavigateToMediaPageCommand { get; }
    public RelayCommand NavigateToCategoryPageCommand { get; }
    public RelayCommand NavigateToGenrePageCommand { get; }
    public RelayCommand NavigateToSettingsPageCommand { get; }
}
