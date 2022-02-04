using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace xcart.Services
{
    [AttributeUsage(validOn:AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyService : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // comparing apikeyheader name and get key value
            if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName,out var potentialKey)){
                context.Result = new UnauthorizedResult();
                return;

            }
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>(key: "ApiKey");

            // comparing apikeyvalues
            if (!apiKey.Equals(potentialKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
