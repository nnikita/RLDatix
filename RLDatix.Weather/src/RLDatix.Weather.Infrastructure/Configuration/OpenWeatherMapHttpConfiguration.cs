using RLDatix.Weather.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLDatix.Weather.Infrastructure.Configuration
{
    public class OpenWeatherMapHttpConfiguration : IHttpConfiguration
    {
        public string ApiKeyHeaderName {  get; set; }

        public string ApiKeyValue { get; set; }

        public string ApiBaseUrl { get; set; }
        public string Path { get; set; }
    }
}
