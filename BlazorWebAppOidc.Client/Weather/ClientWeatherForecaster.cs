using System.Net.Http.Json;

namespace BlazorWebAppOidc.Client.Weather;

internal sealed class ClientWeatherForecaster : IWeatherForecaster
{
    private readonly HttpClient httpClient;

    public ClientWeatherForecaster(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weather-forecast") ??
            throw new IOException("No weather forecast!");
    }
}
