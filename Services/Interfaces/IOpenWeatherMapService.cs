using WeatherWise.Services.DTOs;

namespace WeatherWise.Services.Interfaces
{
    public interface IOpenWeatherMapService
    {
        Task<EnvironmentalData> GetEnvironmentalDataAsync(string city);
    }
}