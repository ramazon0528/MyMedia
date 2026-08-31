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

    public async Task AddAsync(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
            return;

        _context.Genres.Remove(genre);

        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Genre>> GetAllAsync() =>
        await _context.Genres.OrderBy(x => x.Name).ToListAsync();

    public async Task<Genre?> GetByIdAsync(int id) => await _context.Genres.FindAsync(id);
}
