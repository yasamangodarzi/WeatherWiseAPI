using Moq;
using WeatherWise.Services.DTOs;
using WeatherWise.Services.Interfaces;
using WeatherWiseAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestWeatherWise;

public class OkResultTest
{
    [Fact]
    public async Task GetCityEnvironmentalInfo_ReturnsOkResult_WithEnvironmentalData()
    {
        // Arrange
        var mockService = new Mock<IOpenWeatherMapService>();
        
        var expectedData = new EnvironmentalData
        {
            Temperature = 25.0,
            Humidity = 50,
            WindSpeed = 3.5,
            AQI = 2,
            MajorPollutants = new Dictionary<string, double>
            {
                { "PM2.5", 12.3 },
                { "PM10", 20.5 },
                { "CO", 0.4 },
                { "NO2", 0.2 },
                { "O3", 15.0 }
            },
            Latitude = 35.7,
            Longitude = 51.4
        };

        mockService
            .Setup(s => s.GetEnvironmentalDataAsync("Tehran"))
            .ReturnsAsync(expectedData);

        var controller = new EnvironmentalController(mockService.Object);

        // Act
        var result = await controller.GetCityEnvironmentalInfo("Tehran");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualData = Assert.IsType<EnvironmentalData>(okResult.Value);
        Assert.Equal(expectedData.Temperature, actualData.Temperature);
    }
}