namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	internal sealed class EndpointRouteContributor : IEndpointRouteContributor
	{
		/// <inheritdoc />
		public void MapRoute(IEndpointRouteBuilder routeBuilder, IApplicationInitializationContext context)
		{
			context.Log("MapSwagger", serviceProvider =>
			{
				HttpApiOptions options = serviceProvider.GetRequiredService<IOptions<HttpApiOptions>>().Value;

				if(options.Swagger.Enabled)
				{
					routeBuilder.MapSwagger();
				}
			});
		}
	}
}
