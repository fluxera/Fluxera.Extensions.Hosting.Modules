namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using MassTransit;

	internal static class OutboxConfigurationExtensions
	{
		/// <summary>
		///     Configures the Entity Framework Outbox on the bus, which can subsequently be used to configure
		///     the transactional outbox on a receive endpoint.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="configure"></param>
		/// <returns></returns>
		public static void AddMongoOutbox(this IBusRegistrationConfigurator configurator, Action<IMongoDbOutboxConfigurator> configure = null)
		{
			CustomMongoDbOutboxConfigurator outboxConfigurator = new CustomMongoDbOutboxConfigurator(configurator);

			outboxConfigurator.Configure(configure);
		}
	}
}
