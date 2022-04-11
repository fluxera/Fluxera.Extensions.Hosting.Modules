namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the given data seeder contributor to the list of contributors.
		/// </summary>
		/// <remarks>
		///     The seeders are only added in a non-production environment.
		/// </remarks>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddHttpClientServiceContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IHttpClientServiceContributor, new()
		{
			HttpClientServiceRegistrationContributorList contributors = services.GetObject<HttpClientServiceRegistrationContributorList>();
			TContributor contributor = new TContributor();
			contributors.Add(contributor);

			return services;
		}
	}
}
