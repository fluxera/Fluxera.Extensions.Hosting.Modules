namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			RabbitMqMessagingOptions messagingOptions = context.Services.GetOptions<RabbitMqMessagingOptions>();
			string connectionString = messagingOptions.ConnectionStrings[messagingOptions.ConnectionStringName];
			RabbitMqConnectionString rabbitConnectionString = new RabbitMqConnectionString(connectionString);

			// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/tree/master/src/HealthChecks.Rabbitmq
			builder.AddRabbitMQ(rabbitConnectionString, name: "RabbitMQ", tags: new string[] { HealthCheckTags.Ready });
		}
	}
}
