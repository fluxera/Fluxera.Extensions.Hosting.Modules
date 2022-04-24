namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRouteEndpointContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IRouteEndpointContributor, new()
		{
			RouteEndpointContributorList contributorList = services.GetObject<RouteEndpointContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}

		public static IServiceCollection AddMvcBuilderContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IMvcBuilderContributor, new()
		{
			MvcBuilderContributorList contributorList = services.GetObject<MvcBuilderContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}
	}
}
