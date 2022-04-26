namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		/// <inheritdoc />
		public void MapRoute(IEndpointRouteBuilder routeBuilder)
		{
			HttpApiOptions options = routeBuilder.ServiceProvider.GetRequiredService<IOptions<HttpApiOptions>>().Value;

			if(options.Swagger.Enabled)
			{
				routeBuilder.MapSwagger();
			}
		}
	}
}
