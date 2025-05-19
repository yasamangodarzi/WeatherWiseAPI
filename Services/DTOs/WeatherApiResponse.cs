namespace WeatherWise.Services.DTOs;
public class WeatherApiResponse
{
    public MainData Main { get; set; }
    public WindData Wind { get; set; }
    public CoordData Coord { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
    public int Humidity { get; set; }
}

public class WindData
{
    public double Speed { get; set; }
}
public class CoordData
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}