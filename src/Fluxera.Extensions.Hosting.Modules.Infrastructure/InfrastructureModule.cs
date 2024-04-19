namespace Fluxera.Extensions.Hosting.Modules.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;

	/// <summary>
	///     The infrastructure module.
	/// </summary>
	[PublicAPI]
	[DependsOn<PersistenceModule>]
	[DependsOn<MessagingModule>]
	public sealed class InfrastructureModule : ConfigureServicesModule
	{
	}
}
