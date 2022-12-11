namespace Example.Application
{
	using Example.Application.Contracts.Services;
	using Example.Application.Contributors;
	using Example.Application.Services;
	using Example.Infrastructure;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	[DependsOn<ExampleInfrastructureModule>]
	[DependsOn<AutoMapperModule>]
	[DependsOn<ApplicationModule>]
	[DependsOn<ConfigurationModule>]
	public class ExampleApplicationModule : ConfigureServicesModule
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
			context.Services.AddTransient<IExampleApplicationService, ExampleApplicationService>();
		}
	}
}
