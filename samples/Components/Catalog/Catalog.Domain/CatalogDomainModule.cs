namespace Catalog.Domain
{
	using Catalog.Domain.ProductAggregate;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     The domain module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class CatalogDomainModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add repositories.
			context.Log("AddRepositories", services =>
				services.TryAddTransient<IProductRepository, ProductRepository>());
		}
	}
}
