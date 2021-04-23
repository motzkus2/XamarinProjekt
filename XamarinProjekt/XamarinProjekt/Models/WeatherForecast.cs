using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProjekt.Models
{
    public class WeatherForecast
    {
        public DateTime Timestamp { get; set; }

        public float Temperatur { get; set; }

        public float Humidity { get; set; }

        public int TemperatureF => 32 + (int)(Temperatur / 0.5556);

        public string Summary { get; set; }
    }
}
