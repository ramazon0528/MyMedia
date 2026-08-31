using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Interfaces;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task DeleteAsync(int id);
    Task EditAsync(Category category);
    Task<Category?> GetByIdAsync(int id);
    Task<ICollection<Category>> GetAllAsync();
}
