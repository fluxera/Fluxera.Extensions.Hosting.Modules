﻿namespace Fluxera.Extensions.Hosting.Modules.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;

	/// <summary>
	///     The infrastructure module.
	/// </summary>
	[PublicAPI]
	[DependsOn<PersistenceModule>]
	[DependsOn<MessagingModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class InfrastructureModule : ConfigureServicesModule
	{
	}
}