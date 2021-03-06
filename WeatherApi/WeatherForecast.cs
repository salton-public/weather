using System;

namespace WeatherApi
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

    public class WeatherForecastWithKelvin : WeatherForecast
    {
        public int TemperatureK => 273 + TemperatureC;
    }
}
