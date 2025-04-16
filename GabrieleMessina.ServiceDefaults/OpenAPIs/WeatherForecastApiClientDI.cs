using Microsoft.Extensions.DependencyInjection;

namespace GabrieleMessina.Web.Api;

public partial class WeatherForecastApiClient
{
    [ActivatorUtilitiesConstructor] // This ctor will be used by DI
    public WeatherForecastApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        BaseUrl = httpClient.BaseAddress?.ToString() ?? string.Empty;
        _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
    }
}