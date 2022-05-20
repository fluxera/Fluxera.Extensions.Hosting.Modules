namespace Fluxera.Extensions.Hosting.Modules.Domain.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;

	[DependsOn(typeof(InMemoryMessagingModule))]
	[DependsOn(typeof(DomainModule))]
	public class TestModule : ConfigureServicesModule
	{
	}
}
