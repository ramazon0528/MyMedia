using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Interfaces;

public interface IGenreRepository
{
    Task<ICollection<Genre>> GetAllAsync();
}
