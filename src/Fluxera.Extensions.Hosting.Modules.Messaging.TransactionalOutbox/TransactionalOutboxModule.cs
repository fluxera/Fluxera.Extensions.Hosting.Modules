namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox
{
	using System.Collections.Generic;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.Contributors;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     A module that enables the EF Core based transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class TransactionalOutboxModule<TContext> : ConfigureServicesModule
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
			IDictionary<string, object> contextItems = context.Items;

			TransactionalOutboxModuleOptions options = context.Services.GetOptions<TransactionalOutboxModuleOptions>();

			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor<TContext>>();
		}
	}
}
