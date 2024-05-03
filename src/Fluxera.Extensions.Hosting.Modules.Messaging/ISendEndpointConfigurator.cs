namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A contract for a send endpoint configuration service.
	/// </summary>
	[PublicAPI]
	public interface ISendEndpointConfigurator
	{
		/// <summary>
		///     Maps a send endpoint for the given message and consumer types.
		/// </summary>
		/// <typeparam name="TConsumer"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		ISendEndpointConfigurator MapSendEndpoint<T, TConsumer>()
			where T : class
			where TConsumer : class, IConsumer<T>;
	}
}
