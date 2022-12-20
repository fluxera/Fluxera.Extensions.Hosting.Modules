namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.EntityFrameworkCore
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.EntityFrameworkCore.Contributors;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     A module that enables the EF Core based transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class EntityFrameworkCoreTransactionalOutboxModule<TContext> : ConfigureServicesModule
		where TContext : DbContext
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
			// Disable the cleanup and delivery services if the multi tenancy module is loaded.
			bool isMultiTenancyModuleLoaded = context.Items.ContainsKey("IsMultiTenancyModuleLoaded");
			if(isMultiTenancyModuleLoaded)
			{
				TransactionalOutboxModuleOptions options = context.Services.GetOptions<TransactionalOutboxModuleOptions>();
				options.InboxCleanupServiceEnabled = false;
				options.DeliveryServiceEnabled = false;
			}

			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor<TContext>>();
		}
	}
}
