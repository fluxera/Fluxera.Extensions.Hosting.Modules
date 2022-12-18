namespace Ordering
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using JetBrains.Annotations;

	[PublicAPI]
	//[DependsOn<CatalogHttpApiModule>]
	//[DependsOn<CatalogMessagingApiModule>]
	//[DependsOn<CatalogApplicationModule>]
	public sealed class OrderingComponent : IModule
	{
	}
}