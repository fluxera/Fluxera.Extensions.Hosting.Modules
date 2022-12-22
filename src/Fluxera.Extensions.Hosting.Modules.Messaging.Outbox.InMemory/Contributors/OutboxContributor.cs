namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.InMemory.Contributors
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A base class for in-memory transactional outbox configuration.
	/// </summary>
	[UsedImplicitly]
	internal sealed class OutboxContributor : IOutboxContributor
	{
		/// <inheritdoc />
		public void ConfigureOutbox(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			configurator.AddInMemoryInboxOutbox();

			context.Logger.LogInMemoryInboxOutboxUsed();
		}
	}
}
