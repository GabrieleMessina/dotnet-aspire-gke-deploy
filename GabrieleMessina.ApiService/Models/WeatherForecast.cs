using System.ComponentModel.DataAnnotations;

namespace GabrieleMessina.ApiService.Models;

public partial class WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public DateOnly Date { get; init; } = Date;
    public int TemperatureC { get; init; } = TemperatureC;
    public string? Summary { get; init; } = Summary;
}