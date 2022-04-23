namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRouteEndpointContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IRouteEndpointContributor, new()
		{
			RouteEndpointContributorList healthCheckContributorList = services.GetObject<RouteEndpointContributorList>();
			TContributor contributor = new TContributor();
			healthCheckContributorList.Add(contributor);

			return services;
		}
	}
}
