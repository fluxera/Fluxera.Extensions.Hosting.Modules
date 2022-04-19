namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Add the repository contributor to the contributors for the given repository name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <param name="repositoryName"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryContributor<T>(this IServiceCollection services, string repositoryName)
			where T : class, IRepositoryContributor, new()
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.NullOrWhiteSpace(repositoryName, nameof(repositoryName));

			RepositoryContributorDictionary contributorDictionary = services.GetObject<RepositoryContributorDictionary>();
			if(!contributorDictionary.ContainsKey(repositoryName))
			{
				contributorDictionary.Add(repositoryName, new List<IRepositoryContributor>());
			}

			contributorDictionary[repositoryName].Add(new T());

			return services;
		}

		/// <summary>
		///     Adds the given repository contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryProviderContributor<T>(this IServiceCollection services)
			where T : class, IRepositoryProviderContributor, new()
		{
			Guard.Against.Null(services, nameof(services));

			RepositoryProviderContributorList contributorList = services.GetObject<RepositoryProviderContributorList>();
			contributorList.Add(new T());

			return services;
		}
	}
}
