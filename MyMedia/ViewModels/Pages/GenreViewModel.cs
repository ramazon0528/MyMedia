using System.Collections.ObjectModel;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;

namespace MyMedia.ViewModels.Pages;

public class GenreViewModel : ViewModelBase
{
    private readonly GenreService _genreService;

    public GenreViewModel(GenreService genreService)
    {
        _genreService = genreService;

        LoadGenresCommand = new(LoadGenresAsync);
        AddGenreCommand = new(AddGenreAsync);
        DeleteGenreCommand = new(DeleteGenreAsync);
        EditGenreCommand = new(EditGenreAsync);
    }

    private async Task LoadGenresAsync()
    {
        Genres.Clear();

        var genres = await _genreService.GetAllAsync();

        foreach (var item in genres)
            Genres.Add(item);
    }

    private async Task AddGenreAsync()
    {
        if (string.IsNullOrEmpty(GenreName))
            return;

        var genre = new Genre() { Name = GenreName };

        await _genreService.AddAsync(genre);
        await LoadGenresAsync();

        GenreName = string.Empty;
    }

    private async Task DeleteGenreAsync()
    {
        if (SelectedGenre == null)
            return;

        await _genreService.DeleteAsync(SelectedGenre.Id);
        await LoadGenresAsync();
    }

    private async Task EditGenreAsync()
    {
        if (SelectedGenre == null)
            return;

        await _genreService.EditAsync(SelectedGenre);
        await LoadGenresAsync();
    }

    public ObservableCollection<Genre> Genres { get; set; } = [];
    public Genre SelectedGenre { get; set; } = null!;
    public string GenreName { get; set; } = string.Empty;

    public AsyncRelayCommand LoadGenresCommand { get; }
    public AsyncRelayCommand AddGenreCommand { get; }
    public AsyncRelayCommand DeleteGenreCommand { get; }
    public AsyncRelayCommand EditGenreCommand { get; }
}
