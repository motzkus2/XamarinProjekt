using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinProjekt.Models;

namespace XamarinProjekt.Services
{
    public interface IBackendService
    {
        [Get("/api/weatherforecast")]
        Task<IEnumerable<WeatherForecast>> GetWeatherForecast();

    }
}
