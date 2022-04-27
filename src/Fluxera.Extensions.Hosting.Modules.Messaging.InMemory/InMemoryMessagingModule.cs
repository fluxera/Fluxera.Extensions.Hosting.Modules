namespace Fluxera.Extensions.Hosting.Modules.Messaging.InMemory
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables in-memory messaging.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class InMemoryMessagingModule : ConfigureServicesModule
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
