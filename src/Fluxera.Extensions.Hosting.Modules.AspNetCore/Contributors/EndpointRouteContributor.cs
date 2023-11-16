namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	[UsedImplicitly]
	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		public int Position => 1000;

		/// <inheritdoc />
		public void MapRoute(IEndpointRouteBuilder endpoints, IApplicationInitializationContext context)
		{
			context.Log("MapControllers", _ => endpoints.MapControllers());
		}
	}
}
