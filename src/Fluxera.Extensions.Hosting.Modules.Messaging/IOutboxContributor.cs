namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A contract for outbox configuration.
	/// </summary>
	[PublicAPI]
	public interface IOutboxContributor
	{
		/// <summary>
		///     Configures the transport the bus uses.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="context"></param>
		void ConfigureOutbox(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context);
	}
}
