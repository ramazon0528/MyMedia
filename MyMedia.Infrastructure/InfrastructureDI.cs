using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMedia.AppLayer.Interfaces;
using MyMedia.Infrastructure.Data;
using MyMedia.Infrastructure.Repositories;

namespace MyMedia.Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IMediaRepository, MediaRepository>();

        return services;
    }
}
