namespace WeatherWise.Services.DTOs;

public class PollutionApiResponse
{
    public List<PollutionData> List { get; set; }
}

public class PollutionData
{
    public MainPollution Main { get; set; }
    public Components Components { get; set; }
}

public class MainPollution
{
    public int AQI { get; set; }
}

public class Components
{
    public double Co { get; set; }
    public double No2 { get; set; }
    public double O3 { get; set; }
    public double Pm25 { get; set; }
    public double Pm10 { get; set; }
}