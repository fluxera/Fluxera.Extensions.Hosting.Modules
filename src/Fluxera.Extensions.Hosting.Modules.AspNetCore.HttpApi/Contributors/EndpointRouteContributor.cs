namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		/// <inheritdoc />
		public void MapRoute(IEndpointRouteBuilder endpoints, IApplicationInitializationContext context)
		{
			context.Log("MapSwagger", _ => endpoints.MapSwagger("/docs/{documentName}/openapi.{json|yaml}"));
		}
	}
}
