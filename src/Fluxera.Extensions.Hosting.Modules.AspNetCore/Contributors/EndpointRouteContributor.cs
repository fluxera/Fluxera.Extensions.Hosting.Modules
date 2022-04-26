namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	[UsedImplicitly]
	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		public void MapRoute(IEndpointRouteBuilder routeBuilder)
		{
			ILogger<EndpointRouteContributor> logger = routeBuilder.ServiceProvider.GetRequiredService<ILogger<EndpointRouteContributor>>();

			logger.LogDebug("UseEndpoints -> MapControllers()");
			routeBuilder.MapControllers();
		}

		public int Position => 1000;
	}
}
