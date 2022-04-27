namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A contract for contributors that configure consumers.
	/// </summary>
	[PublicAPI]
	public interface IConsumersContributor
	{
		/// <summary>
		///     Configure consumers.
		/// </summary>
		/// <param name="configurator"></param>
		void ConfigureConsumers(IRegistrationConfigurator configurator);
	}
}
