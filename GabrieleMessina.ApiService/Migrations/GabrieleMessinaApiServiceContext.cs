using Microsoft.EntityFrameworkCore;
using GabrieleMessina.ApiService.Models;

namespace GabrieleMessina.ApiService.Migrations
{
    public class GabrieleMessinaApiServiceContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecast { get; set; } = default!;

        public GabrieleMessinaApiServiceContext (DbContextOptions<GabrieleMessinaApiServiceContext> options)
            : base(options)
        {
        }
    }
}
