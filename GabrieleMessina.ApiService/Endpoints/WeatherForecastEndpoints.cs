using Microsoft.EntityFrameworkCore;
using GabrieleMessina.ApiService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using GabrieleMessina.ApiService.Migrations;
namespace GabrieleMessina.ApiService.Endpoints;

public static class WeatherForecastEndpoints
{
    public static void MapWeatherForecastEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/WeatherForecast").WithTags(nameof(WeatherForecast)).WithOpenApi();

        group.MapGet("/", async (GabrieleMessinaApiServiceContext db) =>
        {
            await db.Database.MigrateAsync();
            return await db.WeatherForecast.ToListAsync();
        })
        .WithName("GetAllWeatherForecasts")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<WeatherForecast>, NotFound>> (string id, GabrieleMessinaApiServiceContext db) =>
        {
            return await db.WeatherForecast.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is WeatherForecast model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetWeatherForecastById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string id, WeatherForecast weatherForecast, GabrieleMessinaApiServiceContext db) =>
        {
            var affected = await db.WeatherForecast
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, weatherForecast.Id)
                    .SetProperty(m => m.Date, weatherForecast.Date)
                    .SetProperty(m => m.TemperatureC, weatherForecast.TemperatureC)
                    .SetProperty(m => m.Summary, weatherForecast.Summary)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateWeatherForecast")
        .WithOpenApi();

        group.MapPost("/", async (WeatherForecast weatherForecast, GabrieleMessinaApiServiceContext db) =>
        {
            db.WeatherForecast.Add(weatherForecast);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/WeatherForecast/{weatherForecast.Id}", weatherForecast);
        })
        .WithName("CreateWeatherForecast")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string id, GabrieleMessinaApiServiceContext db) =>
        {
            var affected = await db.WeatherForecast
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteWeatherForecast")
        .WithOpenApi();
    }
}
