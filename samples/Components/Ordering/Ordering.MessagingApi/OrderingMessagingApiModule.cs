namespace Ordering.MessagingApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;
	using Ordering.MessagingApi.Contributors;

	[PublicAPI]
	[DependsOn<MessagingModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class OrderingMessagingApiModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the consumers contributor.
			context.Services.AddConsumersContributor<ConsumersContributor>();
		}
	}
}
