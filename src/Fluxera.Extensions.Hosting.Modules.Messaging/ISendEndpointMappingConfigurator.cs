namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	[PublicAPI]
	public interface ISendEndpointMappingConfigurator
	{
		ISendEndpointMappingConfigurator MapSendEndpoint<T, TConsumer>()
			where T : class
			where TConsumer : class, IConsumer<T>;
	}
}
