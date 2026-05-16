using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;

namespace Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEY
            = "X-API-KEY";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var apiKeyRepository = context.HttpContext.RequestServices.GetRequiredService<IApiKeyRepository>();
            var appSettingsApiKey = await apiKeyRepository.GetKeysAsync();

            foreach (var key in appSettingsApiKey)
            {
                if (BCrypt.Net.BCrypt.Verify(extractedApiKey, key.KeyHash))
                {
                    await next();
                    return;
                }
            }

            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
        }
    }
}