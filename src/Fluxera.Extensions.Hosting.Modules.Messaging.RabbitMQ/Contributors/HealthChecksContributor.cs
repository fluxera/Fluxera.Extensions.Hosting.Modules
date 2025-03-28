namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using global::RabbitMQ.Client;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using Fluxera.Utilities;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			RabbitMqMessagingOptions messagingOptions = context.Services.GetOptions<RabbitMqMessagingOptions>();
			string connectionString = messagingOptions.ConnectionStrings[messagingOptions.ConnectionStringName];
			RabbitMqConnectionString rabbitConnectionString = new RabbitMqConnectionString(connectionString);

			// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/tree/master/src/HealthChecks.Rabbitmq
			builder.AddRabbitMQ(_ => CreateConnection(), name: "RabbitMQ", tags: [ HealthCheckTags.Ready ]);
			return;

			IConnection CreateConnection()
			{
				ConnectionFactory factory = new ConnectionFactory
				{
					Uri = new Uri(rabbitConnectionString),
				};
				return AsyncHelper.RunSync(() => factory.CreateConnectionAsync());
			}
		}
	}
}
