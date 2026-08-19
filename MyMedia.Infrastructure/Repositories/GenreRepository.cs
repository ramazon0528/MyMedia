using Microsoft.EntityFrameworkCore;
using MyMedia.AppLayer.Interfaces;
using MyMedia.Domain.Entities;
using MyMedia.Infrastructure.Data;

namespace MyMedia.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Genre>> GetAllAsync() =>
        await _context.Genres.OrderBy(x => x.Name).ToListAsync();
}
