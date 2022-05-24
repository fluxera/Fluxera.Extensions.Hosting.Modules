namespace Example.Application
{
	using Example.Application.Contracts.Services;
	using Example.Application.Contributors;
	using Example.Application.Services;
	using Example.Domain;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	[DependsOn(typeof(ExampleDomainModule))]
	[DependsOn(typeof(AutoMapperModule))]
	[DependsOn(typeof(PersistenceModule))]
	[DependsOn(typeof(ApplicationModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public class ExampleApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>("Default");

			// Add the consumers contributor.
			context.Services.AddConsumersContributor<ConsumersContributor>();

			// Add the mapping profile contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the application services.
			context.Services.TryAddTransient<IExampleApplicationService, ExampleApplicationService>();
		}
	}
}
