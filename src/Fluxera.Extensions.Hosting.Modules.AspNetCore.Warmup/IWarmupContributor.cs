namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that provide warmup routines.
	/// </summary>
	[PublicAPI]
	public interface IWarmupContributor
	{
		/// <summary>
		///     Creates endpoint init instances.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		IEnumerable<EndpointInitDescriptor> CreateEndpointInit(IServiceConfigurationContext context);
	}
}
