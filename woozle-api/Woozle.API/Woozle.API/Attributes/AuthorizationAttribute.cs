using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Woozle.API.Common;
using Woozle.API.Common.Extensions;

namespace Woozle.API.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		var isTokenExtracted = context.HttpContext.Request.Headers.TryGetValue(Constants.Authorization, out var token);
		if (!isTokenExtracted || token.SingleOrDefault().IsNullOrEmpty())
		{
			context.Result = new BadRequestResult();
		}
	}
}
