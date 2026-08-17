namespace MyMedia.App.DTOs;

public enum MediaSort
{
    Name,
    Rating,
    Date,
}

public class MediaFilter
{
    public string? SearchText { get; set; }
    public int? CategoryId { get; set; }
    public int? GenreId { get; set; }
    public decimal? Rating { get; set; }

    public int Page { get; set; } = 1;
    public int ItemsPerPage { get; set; } = 20;

    public MediaSort SortBy { get; set; } = MediaSort.Name;
    public bool SortDescending { get; set; }
}
