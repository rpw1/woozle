using Microsoft.Extensions.DependencyInjection;

namespace Woozle.API.Features;

public static class FeatureServiceRegistration
{
	public static IServiceCollection RegisterFeatureServices(this IServiceCollection services)
	{
		services.RegisterDecoratedServices();

		return services;
	}
}
