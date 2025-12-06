using Microsoft.Extensions.DependencyInjection;
using Woozle.API.Common.Attributes;
using Woozle.API.Common.Models;

namespace Woozle.API.Common.Extensions;

public static class ServiceRegistrationExtensions
{
	public static void RegisterDecoratedServices(this IServiceCollection services)
	{
		AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(services => services.GetTypes())
			.Where(service => service.IsDefined(typeof(ServiceRegistrationAttribute), false))
			.Select(service => new
			{
				Interface = service.GetInterface($"I{service.Name}"),
				Implementation = service,
			})
			.Where(registration => registration.Interface is not null)
			.ToList()
			.ForEach(registration =>
			{
				if (Attribute.GetCustomAttribute(registration.Implementation, typeof(ServiceRegistrationAttribute)) is ServiceRegistrationAttribute lifetime)
				{
					_ = lifetime.ServiceLifeTimeRegistration switch
					{
						ServiceLifeTimeRegistrationType.Tranient => services.AddTransient(registration.Interface!, registration.Implementation),
						ServiceLifeTimeRegistrationType.Scoped => services.AddScoped(registration.Interface!, registration.Implementation),
						ServiceLifeTimeRegistrationType.Singleton => services.AddSingleton(registration.Interface!, registration.Implementation),
						_ => throw new InvalidOperationException("Incorrect service life time registration type provided.")
					};
				}
			});

	}
}
