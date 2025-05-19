using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using  WeatherWise.Services.DTOs;
using WeatherWise.Services.Interfaces;

namespace WeatherWise.Services.Service;

public class OpenWeatherMapService : IOpenWeatherMapService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public OpenWeatherMapService(IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"];
        _baseUrl = configuration["OpenWeatherMap:BaseUrl"];
    }

    public async Task<EnvironmentalData> GetEnvironmentalDataAsync(string city)
    {

        var weatherUrl = $"{_baseUrl}weather?q={city}&appid={_apiKey}&units=metric";
        var weatherResponse = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(weatherUrl);

        if (weatherResponse == null)
            throw new Exception("Weather data not available");

 
        var pollutionUrl = $"{_baseUrl}air_pollution?lat={weatherResponse.Coord.Lat}&lon={weatherResponse.Coord.Lon}&appid={_apiKey}";
        var pollutionResponse = await _httpClient.GetFromJsonAsync<PollutionApiResponse>(pollutionUrl);

        if (pollutionResponse == null)
            throw new Exception("Pollution data not available");

        return new EnvironmentalData
        {
            Temperature = weatherResponse.Main.Temp,
            Humidity = weatherResponse.Main.Humidity,
            WindSpeed = weatherResponse.Wind.Speed,
            AQI = pollutionResponse.List[0].Main.AQI,
            MajorPollutants = new Dictionary<string, double>
            {
                { "PM2.5", pollutionResponse.List[0].Components.Pm25 },
                { "PM10", pollutionResponse.List[0].Components.Pm10 },
                { "CO", pollutionResponse.List[0].Components.Co },
                { "NO2", pollutionResponse.List[0].Components.No2 },
                { "O3", pollutionResponse.List[0].Components.O3 }
            },
            Latitude = weatherResponse.Coord.Lat,
            Longitude = weatherResponse.Coord.Lon
        };
    }
}



