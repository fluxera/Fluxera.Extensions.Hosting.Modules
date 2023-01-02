namespace Ordering.Application
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Ordering.Application.Contracts.Orders;
	using Ordering.Application.Contributors;
	using Ordering.Application.Orders;
	using Ordering.Infrastructure;

	[PublicAPI]
	[DependsOn<OrderingInfrastructureModule>]
	[DependsOn<AutoMapperModule>]
	[DependsOn<ApplicationModule>]
	[DependsOn<ConfigurationModule>]
	public class OrderingApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the mapping profile contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the MediatR services.
			context.Services.AddMediatR();

			// Add application services.
			context.Services.AddTransient<IOrderApplicationService, OrderApplicationService>();
		}
	}
}
