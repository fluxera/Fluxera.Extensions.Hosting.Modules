namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the EFCore transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn<TransactionalOutboxModule>]
	public sealed class EntityFrameworkCoreTransactionalOutboxModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor>();
		}
	}
}
