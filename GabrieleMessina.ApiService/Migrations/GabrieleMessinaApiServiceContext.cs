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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

            optionsBuilder
                .UseNpgsql("postgresdb")
                .UseSeeding((context, _) =>
                {
                    if (!context.Set<WeatherForecast>().Any())
                    {
                        var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                        .ToArray();
                        context.Set<WeatherForecast>().AddRange(forecast);
                        context.SaveChanges();
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    if (!await context.Set<WeatherForecast>().AnyAsync(cancellationToken))
                    {
                        var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                        .ToArray();
                        await context.Set<WeatherForecast>().AddRangeAsync(forecast);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                });
        }
    }
}
