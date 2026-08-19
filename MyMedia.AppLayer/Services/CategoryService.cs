using MyMedia.AppLayer.Interfaces;
using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ICollection<Category>> GetAllAsync() =>
        await _categoryRepository.GetAllAsync();
}
