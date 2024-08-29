
using MediatR;
using RLDatix.Weather.Application.Interfaces;
using System.Net.Http.Json;

namespace RLDatix.Weather.Application.WeatherForecasts.Queries
{
    public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, WeatherForecastResponse>
    {
        private readonly IExternalApiSerevice _externalApiSerevice;

        public GetWeatherForecastsQueryHandler(IExternalApiSerevice externalApiSerevice)
        {
            _externalApiSerevice = externalApiSerevice;
        }

        public async Task<WeatherForecastResponse> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
        {
            return await _externalApiSerevice.GetWeatherInfo(request.Lat, request.Lon, cancellationToken);
            
        }
    }
}
