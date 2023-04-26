namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the given http client service contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddHttpClientServiceContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IHttpClientServiceContributor, new()
		{
			HttpClientServiceContributorList contributorList = services.GetObject<HttpClientServiceContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IHttpClientServiceContributor));
			}

			return services;
		}

		/// <summary>
		///     Adds the given http client builder contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddHttpClientBuilderContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IHttpClientBuilderContributor, new()
		{
			HttpClientBuilderContributorList contributorList = services.GetObject<HttpClientBuilderContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IHttpClientBuilderContributor));
			}

			return services;
		}
	}
}
