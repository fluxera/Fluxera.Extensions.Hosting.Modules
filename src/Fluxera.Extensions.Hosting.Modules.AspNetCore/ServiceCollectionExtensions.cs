namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Add a route endpoint contributor.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddEndpointRouteContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IEndpointRouteContributor, new()
		{
			RouteEndpointContributorList contributorList = services.GetObjectOrDefault<RouteEndpointContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger?.LogWarning("The contributor list for {Contributor} was not available.", typeof(IEndpointRouteContributor));
			}

			return services;
		}

		/// <summary>
		///     Add a mvc builder contributor.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMvcBuilderContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IMvcBuilderContributor, new()
		{
			MvcBuilderContributorList contributorList = services.GetObjectOrDefault<MvcBuilderContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger?.LogWarning("The contributor list for {Contributor} was not available.", typeof(IMvcBuilderContributor));
			}

			return services;
		}
	}
}
