using System.ComponentModel.DataAnnotations;

namespace GabrieleMessina.Web.Api;

public partial class WeatherForecast
{
    public WeatherForecast(DateTimeOffset date, int temperatureC, string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}