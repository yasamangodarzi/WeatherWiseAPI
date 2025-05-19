namespace WeatherWise.Services.DTOs;

public class EnvironmentalData
{
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public int AQI { get; set; }
    public Dictionary<string, double> MajorPollutants { get; set; } = new();
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}