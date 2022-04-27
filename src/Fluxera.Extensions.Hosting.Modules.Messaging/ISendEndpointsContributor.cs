namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface ISendEndpointsContributor
	{
		void Configure(ISendEndpointMappingConfigurator sendEndpointMapping);
	}
}
