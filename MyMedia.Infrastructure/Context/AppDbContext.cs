using Microsoft.EntityFrameworkCore;
using MyMedia.Domain.Entities;

namespace MyMedia.Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Media> Medias { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(c =>
        {
            c.HasMany(x => x.Medias).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        });

        modelBuilder.Entity<Genre>(g =>
        {
            g.HasMany(x => x.Medias).WithOne(x => x.Genre).HasForeignKey(x => x.GenreId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
