using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAllAsync();
}
