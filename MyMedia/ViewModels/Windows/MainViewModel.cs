using System.Collections.ObjectModel;
using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;

namespace MyMedia.ViewModels.Windows;

public class MainViewModel : ViewModelBase
{
    private readonly MediaService _mediaService;
    private readonly CategoryService _categoryService;
    private readonly GenreService _genreService;

    private MediaFilter _filter = new() { ItemsPerPage = 2 };

    public MainViewModel(
        MediaService mediaService,
        CategoryService categoryService,
        GenreService genreService
    )
    {
        _mediaService = mediaService;
        _categoryService = categoryService;
        _genreService = genreService;

        LoadCommand = new AsyncRelayCommand(InitializeAsync);
        PrevPageCommand = new AsyncRelayCommand(PrevPageAsync, () => HasPrevPage);
        NextPageCommand = new AsyncRelayCommand(NextPageAsync, () => HasNextPage);
        SearchCommand = new AsyncRelayCommand(SearchAsync);
        ApplyFiltersCommand = new AsyncRelayCommand(ApplyFiltersAsync);
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

        CurrentPage = 1;

        await LoadMediasAsync();
    }

    public ObservableCollection<Media> Medias { get; } = [];
    public ObservableCollection<Category> Categories { get; } = [];
    public ObservableCollection<Genre> Genres { get; } = [];

    public Category? SelectedCategory { get; set; }
    public Genre? SelectedGenre { get; set; }

    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }

    public bool HasPrevPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public string SearchText { get; set; } = string.Empty;

    public AsyncRelayCommand LoadCommand { get; }
    public AsyncRelayCommand PrevPageCommand { get; }
    public AsyncRelayCommand NextPageCommand { get; }
    public AsyncRelayCommand SearchCommand { get; }
    public AsyncRelayCommand ApplyFiltersCommand { get; }
}
