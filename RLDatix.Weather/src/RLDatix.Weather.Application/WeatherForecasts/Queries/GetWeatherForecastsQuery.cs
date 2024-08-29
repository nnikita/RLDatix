
using MediatR;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace RLDatix.Weather.Application.WeatherForecasts.Queries
{
    public class GetWeatherForecastsQuery : IRequest<WeatherForecastResponse>
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

    }
    
}



