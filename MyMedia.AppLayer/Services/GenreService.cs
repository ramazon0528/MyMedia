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

    public async Task AddAsync(Genre genre) => await _genreRepository.AddAsync(genre);

    public async Task DeleteAsync(int id) => await _genreRepository.DeleteAsync(id);

    public async Task EditAsync(Genre genre) => await _genreRepository.EditAsync(genre);

    public async Task GetByIdAsync(int id) => await _genreRepository.GetByIdAsync(id);
}
