namespace MyMedia.Domain.Entities;

public class Media
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Rating { get; set; }
    public int CategoryId { get; set; }
    public int GenreId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = true;
    public DateTime Date { get; set; } = DateTime.Now;

    public Category? Category { get; set; }
    public Genre? Genre { get; set; }
}
