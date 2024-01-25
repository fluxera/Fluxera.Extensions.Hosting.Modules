namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contributors;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using Fluxera.Extensions.Hosting.Modules.Localization;
	using JetBrains.Annotations;

	/// <summary>
	///     The application module.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(DomainModule))]
	[DependsOn(typeof(AutoMapperModule))]
	[DependsOn(typeof(LocalizationModule))]
	public sealed class ApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add mapping contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();
		}
	}
}
