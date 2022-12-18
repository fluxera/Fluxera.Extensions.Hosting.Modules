namespace Catalog
{
	using Catalog.Application;
	using Catalog.HttpApi;
	using Catalog.MessagingApi;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn<CatalogHttpApiModule>]
	[DependsOn<CatalogMessagingApiModule>]
	[DependsOn<CatalogApplicationModule>]
	public sealed class CatalogComponent : IModule
	{
	}
}