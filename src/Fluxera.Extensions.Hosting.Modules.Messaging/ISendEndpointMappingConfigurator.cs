namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A contract for a send endpoint configuration service.
	/// </summary>
	[PublicAPI]
	public interface ISendEndpointMappingConfigurator
	{
		/// <summary>
		///     Maps a send endpoint for the given message and consumer types.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TConsumer"></typeparam>
		/// <returns></returns>
		ISendEndpointMappingConfigurator MapSendEndpoint<T, TConsumer>()
			where T : class
			where TConsumer : class, IConsumer<T>;
	}
}
