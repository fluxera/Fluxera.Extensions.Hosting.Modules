namespace Fluxera.Extensions.Hosting.Modules.Messaging.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;

	[DependsOn(typeof(MessagingModule))]
	[DependsOn(typeof(InMemoryMessagingModule))]
	public class TestModule : ConfigureServicesModule
	{
	}
}
