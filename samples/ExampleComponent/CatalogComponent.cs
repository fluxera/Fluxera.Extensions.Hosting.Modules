namespace CatalogComponent
{
	using Catalog.Application;
	using Catalog.HttpApi;
	using Catalog.MessagingApi;
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