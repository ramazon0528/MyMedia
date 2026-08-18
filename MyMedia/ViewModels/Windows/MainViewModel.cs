using System.Collections.ObjectModel;
using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Services;
using MyMedia.Domain.Entities;

namespace MyMedia.ViewModels.Windows;

public class MainViewModel
{
    private readonly MediaService _mediaService;

    public MainViewModel(MediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task LoadMediasAsync()
    {
        var filter = new MediaFilter() { Page = 1, ItemsPerPage = 20 };

        var medias = await _mediaService.GetAllAsync(filter);

        Medias.Clear();

        foreach (var item in medias.Items)
            Medias.Add(item);
    }

    public ObservableCollection<Media> Medias { get; } = [];
}
