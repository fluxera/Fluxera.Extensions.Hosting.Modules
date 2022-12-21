namespace Fluxera.Extensions.Hosting.Modules.Messaging.AzureServiceBus.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
			AzureServiceBusMessagingOptions messagingOptions = context.Services.GetOptions<AzureServiceBusMessagingOptions>();
			string connectionString = messagingOptions.ConnectionStrings[messagingOptions.ConnectionStringName];

			//builder.AddAzureServiceBusQueue(connectionString, "AzureService", tags: new string[]
			//{
			//	HealthCheckTags.Ready
			//});
		}
	}
}
