using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Interfaces;
using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Services;

public class MediaService
{
    private readonly IMediaRepository _mediaRepository;

    public MediaService(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    public async Task AddAsync(Media media) => await _mediaRepository.AddAsync(media);

    public async Task EditAsync(Media media) => await _mediaRepository.EditAsync(media);

    public async Task DeleteAsync(int id) => await _mediaRepository.DeleteAsync(id);

    public async Task<Media?> GetByIdAsync(int id) => await _mediaRepository.GetByIdAsync(id);

    public async Task<PagedResult<Media>> GetAllAsync(MediaFilter filter) =>
        await _mediaRepository.GetAllAsync(filter);
}
