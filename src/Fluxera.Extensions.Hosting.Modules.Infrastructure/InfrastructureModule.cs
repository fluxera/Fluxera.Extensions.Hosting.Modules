namespace Fluxera.Extensions.Hosting.Modules.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using Fluxera.Extensions.Hosting.Modules.FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.MediatR;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;

	/// <summary>
	///     The infrastructure module.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	[DependsOn<PersistenceModule>]
	[DependsOn<MessagingModule>]
	[DependsOn<ApplicationModule>]
	[DependsOn<MediatrModule>]
	[DependsOn<FluentValidationModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class InfrastructureModule : ConfigureServicesModule
	{
	}
}
