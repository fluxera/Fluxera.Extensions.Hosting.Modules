namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contributors;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using Fluxera.Extensions.Hosting.Modules.FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.MediatR;
	using JetBrains.Annotations;

	/// <summary>
	///     The application module.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	[DependsOn<AutoMapperModule>]
	[DependsOn<MediatrModule>]
	[DependsOn<FluentValidationModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class ApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add mapping contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();

			// Add MediatR contributor.
			context.Services.AddMediatrContributor<MediatrContributor>();
		}
	}
}
