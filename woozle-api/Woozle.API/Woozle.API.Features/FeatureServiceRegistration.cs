using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Woozle.API.Common.Extensions;

namespace Woozle.API.Features;

public static class FeatureServiceRegistration
{
	public static IServiceCollection RegisterFeatureServices(this IServiceCollection services)
	{
		services.RegisterDecoratedServices();

		return services;
	}
}
