using Microsoft.EntityFrameworkCore;
using MyMedia.AppLayer.Interfaces;
using MyMedia.Domain.Entities;
using MyMedia.Infrastructure.Data;

namespace MyMedia.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
            return;

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Category>> GetAllAsync() =>
        await _context.Categories.OrderBy(x => x.Name).ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);
}
