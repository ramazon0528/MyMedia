using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Interfaces;

public interface IGenreRepository
{
    Task AddAsync(Genre genre);
    Task DeleteAsync(int id);
    Task EditAsync(Genre genre);
    Task<Genre?> GetByIdAsync(int id);
    Task<ICollection<Genre>> GetAllAsync();
}
