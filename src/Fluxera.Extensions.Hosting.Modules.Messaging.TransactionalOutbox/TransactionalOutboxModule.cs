namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox
{
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

			// Add the outbox contributor.
			context.Services.AddOutboxContributor<OutboxContributor<TContext>>();
		}
	}
}
