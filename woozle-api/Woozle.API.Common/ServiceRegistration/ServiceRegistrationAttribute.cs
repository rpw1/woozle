namespace Woozle.API.Common.ServiceRegistration;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ServiceRegistrationAttribute(ServiceLifeTimeRegistrationType serviceLifeTimeRegistration) : Attribute
{
	public ServiceLifeTimeRegistrationType ServiceLifeTimeRegistration { get; set; } = serviceLifeTimeRegistration;
}
