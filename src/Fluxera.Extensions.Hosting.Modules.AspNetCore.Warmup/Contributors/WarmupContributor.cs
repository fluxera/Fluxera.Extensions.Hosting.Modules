namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.Contributors
{
	using System.Collections.Generic;

	internal sealed class WarmupContributor : IWarmupContributor
	{
		/// <inheritdoc />
		public IEnumerable<EndpointInitDescriptor> CreateEndpointInit(IServiceConfigurationContext context)
		{
			yield return new EndpointInitDescriptor(typeof(ServiceDependenciesEndpointInit));
			yield return new EndpointInitDescriptor(typeof(ActionsEndpointInit));
		}
	}
}
