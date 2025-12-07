using Microsoft.Extensions.DependencyInjection;

namespace Woozle.API.Common.ServiceRegistration;

public static class RegisterDecoratedServicesFn
{
	extension(IServiceCollection services)
	{
        public void RegisterDecoratedServices()
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
}
