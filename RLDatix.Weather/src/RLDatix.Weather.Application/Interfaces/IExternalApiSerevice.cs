using RLDatix.Weather.Application.WeatherForecasts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLDatix.Weather.Application.Interfaces
{
    public interface IExternalApiSerevice
    {
        Task<WeatherForecastResponse> GetWeatherInfo(double latitude, double longitude, CancellationToken cancellationToken);
    }
}
