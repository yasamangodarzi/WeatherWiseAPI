using Microsoft.AspNetCore.Mvc;
using WeatherWise.Services.Interfaces;

namespace WeatherWiseAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnvironmentalController : ControllerBase
{
    private readonly IOpenWeatherMapService _weatherService;

    public EnvironmentalController(IOpenWeatherMapService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetCityEnvironmentalInfo(string city)
    {
        try
        {
            
            var result = await _weatherService.GetEnvironmentalDataAsync(city);
            return Ok(result);
        }
        catch (HttpRequestException)
        {
            return NotFound(new { message = "City not found or API error." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        }
    }
}