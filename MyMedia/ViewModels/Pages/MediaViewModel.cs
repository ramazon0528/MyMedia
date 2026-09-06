using System.Collections.ObjectModel;
using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;
using MyMedia.Helpers;
using MyMedia.Services;
using MyMedia.Services.Interfaces;

namespace MyMedia.ViewModels.Pages;

public class MediaViewModel : ViewModelBase
{
    private readonly MediaService _mediaService;
    private readonly CategoryService _categoryService;
    private readonly GenreService _genreService;
    private readonly NavigationService _navigationService;
    private readonly MediaFilter _filter = new() { ItemsPerPage = 20 };

    private readonly AddMediaViewModel _addMediaViewModel;

    public MediaViewModel(
        MediaService mediaService,
        CategoryService categoryService,
        GenreService genreService,
        NavigationService navigationService,
        AddMediaViewModel addMediaViewModel
    )
    {
        _mediaService = mediaService;
        _categoryService = categoryService;
        _genreService = genreService;
        _navigationService = navigationService;
        _addMediaViewModel = addMediaViewModel;

        LoadMediasCommand = new(InitializeAsync);
        ApplyFiltersCommand = new(ApplyFiltersAsync);
        ResetFiltersCommand = new(ResetFiltersAsync);
        SearchMediasCommand = new(SearchAsync);
        DeleteMediaCommand = new(DeleteMediaAsync);
        EditMediaCommand = new(EditMediaAsync);
        NextPageCommand = new(NextPageAsync);
        PrevPageCommand = new(PrevPageAsync);

        AddMediaCommand = new(async () => await _navigationService.NavigateTo(_addMediaViewModel));
    }

    #region Methods
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

    private async Task InitializeAsync()
    {
        await LoadFiltersAsync();
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

    private async Task DeleteMediaAsync()
    {
        if (SelectedMedia == null)
            return;

        await _mediaService.DeleteAsync(SelectedMedia.Id);
        await ApplyFiltersAsync();
    }

    private async Task EditMediaAsync()
    {
        if (MediaForEdit == null)
            return;

        await _mediaService.EditAsync(MediaForEdit);
        await ApplyFiltersAsync();

        MediaForEdit = null!;
    }

    private async Task SearchAsync()
    {
        _filter.SearchText = SearchText;

        CurrentPage = 1;

        await LoadMediasAsync();
    }

    #endregion

    #region Collections

    public ObservableCollection<Media> Medias { get; set; } = [];
    public ObservableCollection<Category> Categories { get; set; } = [];
    public ObservableCollection<Genre> Genres { get; set; } = [];
    public ObservableCollection<string> SortOptions { get; } = ["Название", "Рейтинг", "Дата"];
    #endregion

    #region Selected Items

    private Media? _media = null!;
    public Media? SelectedMedia
    {
        get => _media;
        set
        {
            if (_media == value)
                return;

            _media = value;
            MediaForEdit = _media;
            OnPropertyChanged();
        }
    }
    public Category? SelectedCategory { get; set; } = null!;
    public Genre? SelectedGenre { get; set; } = null!;
    public string SelectedSortOption { get; set; } = string.Empty;

    #endregion

    #region Commands

    public AsyncRelayCommand LoadMediasCommand { get; }
    public AsyncRelayCommand ApplyFiltersCommand { get; }
    public AsyncRelayCommand ResetFiltersCommand { get; }
    public AsyncRelayCommand SearchMediasCommand { get; }
    public AsyncRelayCommand DeleteMediaCommand { get; }
    public AsyncRelayCommand EditMediaCommand { get; }
    public RelayCommand AddMediaCommand { get; }
    #endregion

    #region Pagination Properties

    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public bool HasPrevPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public AsyncRelayCommand PrevPageCommand { get; }
    public AsyncRelayCommand NextPageCommand { get; }
    #endregion

    public bool SortDescending { get; set; }
    public string SearchText { get; set; } = string.Empty;
    public Media? MediaForEdit { get; set; } = null!;
}
