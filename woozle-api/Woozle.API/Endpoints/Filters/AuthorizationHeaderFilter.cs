using Woozle.API.Common;
using Woozle.API.Common.Extensions;

namespace Woozle.API.Endpoints.Filters;

public class AuthorizationHeaderFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var isTokenExtracted = context.HttpContext.Request.Headers.TryGetValue(Constants.Authorization, out var token);
        if (!isTokenExtracted || token.SingleOrDefault().IsNullOrEmpty())
        {
            return TypedResults.BadRequest();
        }

        return await next(context);
    }
}
