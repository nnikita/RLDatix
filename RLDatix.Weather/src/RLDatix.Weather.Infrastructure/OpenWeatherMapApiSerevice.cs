using Microsoft.Extensions.Options;
using RLDatix.Weather.Application.Configuration;
using RLDatix.Weather.Application.Interfaces;
using RLDatix.Weather.Application.WeatherForecasts;
using RLDatix.Weather.Application.WeatherForecasts.Queries;
using RLDatix.Weather.Infrastructure.Configuration;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading;
using System.Web;
using RLDatix.Weather.Infrastructure.Constants;

namespace RLDatix.Weather.Infrastructure
{
    public class OpenWeatherMapApiSerevice : IExternalApiSerevice
    {
        private readonly IOptions<OpenWeatherMapHttpConfiguration> _OpenWeatherMapHttpConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;
        public OpenWeatherMapApiSerevice(IHttpClientFactory httpClientFactory,IOptions<OpenWeatherMapHttpConfiguration> openWeatherMapHttpConfiguration)
        {
            _OpenWeatherMapHttpConfiguration = openWeatherMapHttpConfiguration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<WeatherForecastResponse> GetWeatherInfo(double latitude,double longitude, CancellationToken cancellationToken)
        {

            var uri = GetUri(latitude, longitude);

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetFromJsonAsync<OpenWeatherMapResponse>(uri, cancellationToken);
            

            return new WeatherForecastResponse
            {
                FeelsLike = response.main.feels_like,
                GroundLevel = response.main.grnd_level,
                Humidity = response.main.humidity,
                Pressure = response.main.pressure,
                SeaLevel = response.main.sea_level,
                Temperature = response.main.temp,
                TempMax = response.main.temp_max,
                TempMin = response.main.temp_min
            };
        }

        private Uri GetUri(double latitude, double longitude)
        {
            var uriBuilder = new UriBuilder(_OpenWeatherMapHttpConfiguration.Value.ApiBaseUrl)
            {
                Path = _OpenWeatherMapHttpConfiguration.Value.Path
            };

            var query = HttpUtility.ParseQueryString(string.Empty);
            query[Constants.Constants.LATITUDE] = latitude.ToString();
            query[Constants.Constants.LONGITUDE] = longitude.ToString();
            query[Constants.Constants.APP_ID] = _OpenWeatherMapHttpConfiguration.Value.ApiKeyValue;

            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }
    }
}
