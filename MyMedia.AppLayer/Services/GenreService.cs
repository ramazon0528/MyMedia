using MyMedia.AppLayer.Interfaces;
using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.Services;

public class GenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ICollection<Genre>> GetAllAsync() => await _genreRepository.GetAllAsync();
}
