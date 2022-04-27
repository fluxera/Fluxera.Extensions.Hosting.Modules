namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables RabbitMQ messaging.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class RabbitMqMessagingModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the transport contributor.
			context.Services.AddTransportContributor<TransportContributor>();
		}
	}
}
