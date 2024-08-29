using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLDatix.Weather.Application.Common.Security
{
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration["AppSettings:ApiKey"];

            if (!context.HttpContext.Request.Headers.TryGetValue(Constants.Constants.API_KEY_HEADER_NAME, out var apiKeyFromRequest))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey.Equals(apiKeyFromRequest)) 
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
