namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using MassTransit;
	using MassTransit.Configuration;

	internal static class OutboxConfigurationExtensions
	{
		/// <summary>
		///     Configures the MongoDB Outbox on the bus, which can subsequently be used to configure
		///     the transactional outbox on a receive endpoint.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="configure"></param>
		/// <returns></returns>
		public static void AddMongoOutbox(this IBusRegistrationConfigurator configurator, Action<IMongoDbOutboxConfigurator> configure = null)
		{
			MongoDbOutboxConfigurator outboxConfigurator = new MongoDbOutboxConfigurator(configurator);

			outboxConfigurator.Configure(configure);
		}
	}
}
