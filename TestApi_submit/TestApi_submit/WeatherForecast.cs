namespace TestApi_submit
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public Int32 TemperatureC { get; set; }

        public Int32 TemperatureF => 32 + (Int32)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}