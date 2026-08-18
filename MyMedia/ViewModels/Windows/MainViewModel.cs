using System.Collections.ObjectModel;
using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;

namespace MyMedia.ViewModels.Windows;

public class MainViewModel : ViewModelBase
{
    private readonly MediaService _mediaService;

    public MainViewModel(MediaService mediaService)
    {
        _mediaService = mediaService;

        LoadCommand = new AsyncRelayCommand(LoadMediasAsync);
        PrevPageCommand = new AsyncRelayCommand(PrevPageAsync, () => HasPrevPage);
        NextPageCommand = new AsyncRelayCommand(NextPageAsync, () => HasNextPage);
    }

    public async Task LoadMediasAsync()
    {
        var filter = new MediaFilter() { Page = CurrentPage, ItemsPerPage = 3 };

        var medias = await _mediaService.GetAllAsync(filter);

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

    public ObservableCollection<Media> Medias { get; } = [];

    private int _currentPage = 1;

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (_currentPage == value)
                return;

            _currentPage = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(HasPrevPage));
            OnPropertyChanged(nameof(HasNextPage));
        }
    }
    private int _totalPages;

    public int TotalPages
    {
        get => _totalPages;
        set
        {
            if (_totalPages == value)
                return;

            _totalPages = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(HasNextPage));
        }
    }

    public bool HasPrevPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public AsyncRelayCommand LoadCommand { get; }
    public AsyncRelayCommand PrevPageCommand { get; }
    public AsyncRelayCommand NextPageCommand { get; }
}
