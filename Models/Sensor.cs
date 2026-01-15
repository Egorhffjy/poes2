namespace SentinelIoT.Models;

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Zone { get; set; } = "";
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public string Status { get; set; } = "Normal"; // Normal, Warning, Critical
}

public class ChartData
{
    public string TimeLabel { get; set; } = "";
    public double Value { get; set; }
}
