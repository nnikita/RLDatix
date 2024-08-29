using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RLDatix.Weather.Application.Common.Security;
using RLDatix.Weather.Application.WeatherForecasts.Queries;

namespace RLDatix.Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediatR)
         {
             _logger = logger;
            _mediator = mediatR;
         }

        [ApiKeyAuth]
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromQuery] GetWeatherForecastsQuery query)
        {

           return Ok(await _mediator.Send(query));
        }
    }
}
