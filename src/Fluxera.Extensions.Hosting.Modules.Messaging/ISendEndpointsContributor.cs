namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributor implementations that configure send endpoints.
	/// </summary>
	[PublicAPI]
	public interface ISendEndpointsContributor
	{
		/// <summary>
		///     Configure send endpoints.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="context"></param>
		void Configure(ISendEndpointMappingConfigurator configurator, IApplicationInitializationContext context);
	}
}
