namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A contract for transport specific configurations.
	/// </summary>
	[PublicAPI]
	public interface ITransportContributor
	{
		/// <summary>
		///     Configures the transport the bus uses.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="context"></param>
		void ConfigureTransport(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context);
	}
}
