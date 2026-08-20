using System.Collections.ObjectModel;
using System.Windows;
using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;
using MyMedia.Services.Interfaces;

namespace MyMedia.ViewModels.Windows;

public class MainViewModel : ViewModelBase
{
    private readonly MediaService _mediaService;
    private readonly CategoryService _categoryService;
    private readonly GenreService _genreService;

    private readonly IThemeService _themeService;
    private readonly IWindowService _windowService;
    private MediaFilter _filter = new() { ItemsPerPage = 4 };

    public MainViewModel(
        MediaService mediaService,
        CategoryService categoryService,
        GenreService genreService,
        IThemeService themeService,
        IWindowService windowService
    )
    {
        _mediaService = mediaService;
        _categoryService = categoryService;
        _genreService = genreService;
        _themeService = themeService;
        _windowService = windowService;

        Themes = new(_themeService.GetThemes());
        _selectedTheme = _themeService.CurrentTheme;

        LoadCommand = new AsyncRelayCommand(InitializeAsync);
        PrevPageCommand = new AsyncRelayCommand(PrevPageAsync, () => HasPrevPage);
        NextPageCommand = new AsyncRelayCommand(NextPageAsync, () => HasNextPage);
        SearchCommand = new AsyncRelayCommand(SearchAsync);
        ApplyFiltersCommand = new AsyncRelayCommand(ApplyFiltersAsync);
        ResetFiltersCommand = new AsyncRelayCommand(ResetFiltersAsync);

        SaveThemeCommand = new RelayCommand(SaveTheme);

        CloseCommand = new(_windowService.Close);
        MaximizeCommand = new(_windowService.Maximize);
        MinimizeCommand = new(_windowService.Minimize);
    }

    private async Task InitializeAsync()
    {
        await LoadFiltersAsync();
        await LoadMediasAsync();
    }

    private async Task LoadFiltersAsync()
    {
        Genres.Clear();
        Categories.Clear();

        var categories = await _categoryService.GetAllAsync();
        var genres = await _genreService.GetAllAsync();

        foreach (var item in categories)
            Categories.Add(item);

        foreach (var item in genres)
            Genres.Add(item);
    }

    private async Task LoadMediasAsync()
    {
        _filter.Page = CurrentPage;

        var medias = await _mediaService.GetAllAsync(_filter);

        Medias.Clear();

        foreach (var item in medias.Items)
            Medias.Add(item);

        TotalPages = medias.TotalPages;

        NextPageCommand.RaiseCanExecuteChanged();
        PrevPageCommand.RaiseCanExecuteChanged();
    }

    private async Task PrevPageAsync()
    {
        if (!HasPrevPage)
            return;

        CurrentPage--;

        await LoadMediasAsync();
    }

    private async Task NextPageAsync()
    {
        if (!HasNextPage)
            return;

        CurrentPage++;

        await LoadMediasAsync();
    }

    private async Task SearchAsync()
    {
        _filter.SearchText = SearchText;

        CurrentPage = 1;

        await LoadMediasAsync();
    }

    private async Task ApplyFiltersAsync()
    {
        _filter.CategoryId = SelectedCategory?.Id;
        _filter.GenreId = SelectedGenre?.Id;
        _filter.SortDescending = SortDescending;
        _filter.SortBy = SelectedSortOption switch
        {
            "Название" => MediaSort.Name,
            "Рейтинг" => MediaSort.Rating,
            "Дата" => MediaSort.Date,
            _ => MediaSort.Name,
        };

        CurrentPage = 1;

        await LoadMediasAsync();
    }

    private async Task ResetFiltersAsync()
    {
        SearchText = string.Empty;
        SelectedCategory = null;
        SelectedGenre = null;

        SortDescending = false;
        SelectedSortOption = "Название";

        _filter.SearchText = SearchText;
        _filter.CategoryId = null;
        _filter.GenreId = null;

        _filter.SortDescending = SortDescending;
        _filter.SortBy = MediaSort.Name;

        CurrentPage = 1;

        await LoadMediasAsync();
    }

    private void SaveTheme()
    {
        if (SelectedTheme == null)
            return;

        _themeService.SaveTheme(SelectedTheme);
    }

    public ObservableCollection<Media> Medias { get; } = [];
    public ObservableCollection<Category> Categories { get; } = [];
    public ObservableCollection<Genre> Genres { get; } = [];

    public ObservableCollection<string> SortOptions { get; } = ["Название", "Рейтинг", "Дата"];
    public string SelectedSortOption { get; set; } = string.Empty;
    public bool SortDescending { get; set; } = false;

    private Category? _category;
    public Category? SelectedCategory
    {
        get => _category;
        set
        {
            _category = value;
            OnPropertyChanged();
            _filter.CategoryId = SelectedCategory?.Id;
            CurrentPage = 1;
            _ = LoadMediasAsync();
        }
    }
    public Genre? SelectedGenre { get; set; }

    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }

    public bool HasPrevPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public string SearchText { get; set; } = string.Empty;

    public List<string> Themes { get; } = [];
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

    public AsyncRelayCommand LoadCommand { get; }
    public AsyncRelayCommand PrevPageCommand { get; }
    public AsyncRelayCommand NextPageCommand { get; }
    public AsyncRelayCommand SearchCommand { get; }
    public AsyncRelayCommand ApplyFiltersCommand { get; }
    public AsyncRelayCommand ResetFiltersCommand { get; }

    public RelayCommand SaveThemeCommand { get; }
    public RelayCommand CloseCommand { get; }
    public RelayCommand MaximizeCommand { get; }
    public RelayCommand MinimizeCommand { get; }
}
