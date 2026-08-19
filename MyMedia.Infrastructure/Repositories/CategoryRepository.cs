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

    public async Task<ICollection<Category>> GetAllAsync() =>
        await _context.Categories.OrderBy(x => x.Name).ToListAsync();
}
