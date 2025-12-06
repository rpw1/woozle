using Woozle.API.Common.Models;

namespace Woozle.API.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ServiceRegistrationAttribute(ServiceLifeTimeRegistrationType serviceLifeTimeRegistration) : Attribute
{
	public ServiceLifeTimeRegistrationType ServiceLifeTimeRegistration { get; set; } = serviceLifeTimeRegistration;
}
