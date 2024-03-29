namespace Catalog.Application
{
	using Catalog.Application.Contracts.Products;
	using Catalog.Application.Contributors;
	using Catalog.Application.Products;
	using Catalog.Infrastructure;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	[DependsOn<CatalogInfrastructureModule>]
	[DependsOn<AutoMapperModule>]
	[DependsOn<ApplicationModule>]
	[DependsOn<ConfigurationModule>]
	public class CatalogApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the mapping profile contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();

			// Add the consumers contributor.
			context.Services.AddConsumersContributor<ConsumersContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the MediatR services.
			context.Services.AddMediatR();

			// Add application services.
			context.Services.AddTransient<IProductApplicationService, ProductApplicationService>();
		}
	}
}
