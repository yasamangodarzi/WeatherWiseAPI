using Moq;
using WeatherWiseAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using WeatherWise.Services.Interfaces;

namespace TestWeatherWise;

public class InvalidCityTest
{
    [Fact]
    public async Task GetCityEnvironmentalInfo_InvalidCity_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IOpenWeatherMapService>();

        mockService
            .Setup(s => s.GetEnvironmentalDataAsync("InvalidCity"))
            .ThrowsAsync(new HttpRequestException("City not found"));

        var controller = new EnvironmentalController(mockService.Object);

        // Act
        var result = await controller.GetCityEnvironmentalInfo("InvalidCity");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var message = notFoundResult.Value?
            .GetType()
            .GetProperty("message")?
            .GetValue(notFoundResult.Value, null) as string;

        Assert.Equal("City not found or API error.", message);

    }
}