using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyMedia.AppLayer.DTOs;
using MyMedia.AppLayer.Interfaces;
using MyMedia.Domain.Entities;
using MyMedia.Infrastructure.Data;

namespace MyMedia.Infrastructure.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly AppDbContext _context;

    public MediaRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(Media media)
    {
        await _context.Medias.AddAsync(media);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var media = await _context.Medias.FindAsync(id);

        if (media is null)
            return;

        _context.Medias.Remove(media);

        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(Media newMedia)
    {
        _context.Medias.Update(newMedia);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<Media>> GetAllAsync(MediaFilter filter)
    {
        IQueryable<Media> query = _context.Medias.Include(x => x.Category).Include(x => x.Genre);

        if (!string.IsNullOrWhiteSpace(filter.SearchText))
            query = query.Where(x => x.Name.ToLower().Contains(filter.SearchText.ToLower()));

        if (filter.CategoryId.HasValue)
            query = query.Where(x => x.CategoryId == filter.CategoryId);

        if (filter.GenreId.HasValue)
            query = query.Where(x => x.GenreId == filter.GenreId);

        if (filter.Rating.HasValue)
            query = query.Where(x => x.Rating >= filter.Rating);

        query = filter.SortBy switch
        {
            MediaSort.Name => filter.SortDescending
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name),

            MediaSort.Rating => filter.SortDescending
                ? query.OrderByDescending(x => x.Rating)
                : query.OrderBy(x => x.Rating),

            MediaSort.Date => filter.SortDescending
                ? query.OrderByDescending(x => x.Date)
                : query.OrderBy(x => x.Date),

            _ => query.OrderBy(x => x.Name),
        };

        var totalItems = await query.CountAsync();
        var page = Math.Max(filter.Page, 1);
        var itemsPerPage = Math.Max(filter.ItemsPerPage, 1);
        var items = await query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

        return new PagedResult<Media>
        {
            Items = items,
            CurrentPage = page,
            TotalItems = totalItems,
            TotalPages = totalPages,
        };
    }

    public async Task<Media?> GetByIdAsync(int id) => await _context.Medias.FindAsync(id);
}
