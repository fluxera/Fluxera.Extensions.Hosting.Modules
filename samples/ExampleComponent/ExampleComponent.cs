namespace ExampleComponent
{
	using Example.Application;
	using Example.HttpApi;
	using Example.MessagingApi;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn<ExampleHttpApiModule>]
	[DependsOn<ExampleMessagingApiModule>]
	[DependsOn<ExampleApplicationModule>]
	public sealed class ExampleComponent : IModule
	{
	}
}