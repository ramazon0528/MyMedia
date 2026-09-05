using System.Collections.ObjectModel;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;
using MyMedia.Services;
using MyMedia.Services.Interfaces;

namespace MyMedia.ViewModels.Pages;

public class AddMediaViewModel : ViewModelBase
{
    private readonly MediaService _mediaService;
    private readonly CategoryService _categoryService;
    private readonly GenreService _genreService;
    private readonly IDialogService _dialogService;
    private readonly IImageService _imageService;

    public AddMediaViewModel(
        MediaService mediaService,
        CategoryService categoryService,
        GenreService genreService,
        IDialogService dialogService,
        IImageService imageService
    )
    {
        _mediaService = mediaService;
        _categoryService = categoryService;
        _genreService = genreService;
        _dialogService = dialogService;
        _imageService = imageService;

        InitializeCommand = new(InitializeFiltersAsync);
        AddMediaCommand = new(AddMediaAsync);

        SelectImageCommand = new(SelectImage);
    }

    private async Task InitializeFiltersAsync()
    {
        Categories.Clear();
        Genres.Clear();

        var categories = await _categoryService.GetAllAsync();
        var genres = await _genreService.GetAllAsync();

        foreach (var item in genres)
            Genres.Add(item);

        foreach (var item in categories)
            Categories.Add(item);
    }

    private async Task AddMediaAsync()
    {
        var media = new Media()
        {
            Name = Name,
            Date = Date,
            ImagePath = ImagePath,
            Rating = Rating,
            IsCompleted = IsCompleted,
            CategoryId = SelectedCategory?.Id,
            GenreId = SelectedGenre?.Id,
        };

        await _mediaService.AddAsync(media);

        Name = string.Empty;
        Date = DateTime.Now;
        Rating = 0;
        ImagePath = string.Empty;
        IsCompleted = true;
    }

    private void SelectImage()
    {
        var sourcePath = _dialogService.ShowDialog();

        if (sourcePath == null)
            return;

        ImagePath = _imageService.CopyImage(sourcePath);
    }

    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public int Rating { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = true;

    public ObservableCollection<Category> Categories { get; } = [];
    public ObservableCollection<Genre> Genres { get; } = [];

    public Category SelectedCategory { get; set; } = null!;
    public Genre SelectedGenre { get; set; } = null!;

    public AsyncRelayCommand InitializeCommand { get; }
    public AsyncRelayCommand AddMediaCommand { get; }

    public RelayCommand SelectImageCommand { get; }
}
