using MyMedia.Domain.Entities;

namespace MyMedia.AppLayer.DTOs;

public class PagedResult<T>
{
    public ICollection<T> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
}
