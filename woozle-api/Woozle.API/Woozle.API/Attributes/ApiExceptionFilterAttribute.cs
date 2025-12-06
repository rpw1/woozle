using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Woozle.API.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
	public override Task OnExceptionAsync(ExceptionContext context)
	{
		var newContext = context.Exception switch
		{
			UnauthorizedAccessException => HandleUnauthorizedAccessExcpetion(context),
			_ => throw new InvalidOperationException($"ApiExcptionFilterAttribute does not support the excption type of {context.Exception.GetType()}")
		};
		return base.OnExceptionAsync(newContext);
	}

	private static ExceptionContext HandleUnauthorizedAccessExcpetion(ExceptionContext context)
	{
		context.Result = new UnauthorizedResult();
		return context;
	}
}
