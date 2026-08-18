using MyMedia.AppLayer.DTOs;
using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Interfaces;

public interface IMediaRepository
{
    Task AddAsync(Media media);
    Task EditAsync(Media newMedia);
    Task DeleteAsync(int id);
    Task<Media?> GetByIdAsync(int id);
    Task<PagedResult<Media>> GetAllAsync(MediaFilter filter);
}
