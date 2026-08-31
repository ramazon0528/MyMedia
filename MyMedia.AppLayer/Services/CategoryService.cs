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

    public async Task<Category?> GetByIdAsync(int id) => await _categoryRepository.GetByIdAsync(id);

    public async Task AddAsync(Category category) => await _categoryRepository.AddAsync(category);

    public async Task DeleteAsync(int id) => await _categoryRepository.DeleteAsync(id);

    public async Task EditAsync(Category category) => await _categoryRepository.EditAsync(category);
}
