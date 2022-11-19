namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Guards;
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
		///     Add the repository context contributor to the contributors for the default repository name.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryContextContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IRepositoryContextContributor, new()
		{
			return services.AddRepositoryContextContributor<TContributor>("Default");
		}

		/// <summary>
		///     Add the repository context contributor to the contributors for the given repository name.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <param name="repositoryName"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryContextContributor<TContributor>(this IServiceCollection services, string repositoryName)
			where TContributor : class, IRepositoryContextContributor, new()
		{
			Guard.Against.Null(services);
			Guard.Against.NullOrWhiteSpace(repositoryName);

			RepositoryContextContributorDictionary contributorDict = services.GetObjectOrDefault<RepositoryContextContributorDictionary>();
			if(contributorDict != null)
			{
				if(!contributorDict.ContainsKey(repositoryName))
				{
					TContributor contributor = new TContributor();
					contributorDict.Add(repositoryName, contributor);
				}
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IRepositoryContextContributor));
			}

			return services;
		}

		/// <summary>
		///     Add the repository contributor to the contributors for the default repository name.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IRepositoryContributor, new()
		{
			return services.AddRepositoryContributor<TContributor>("Default");
		}

		/// <summary>
		///     Add the repository contributor to the contributors for the given repository name.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <param name="repositoryName"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryContributor<TContributor>(this IServiceCollection services, string repositoryName)
			where TContributor : class, IRepositoryContributor, new()
		{
			Guard.Against.Null(services);
			Guard.Against.NullOrWhiteSpace(repositoryName);

			RepositoryContributorDictionary contributorDict = services.GetObjectOrDefault<RepositoryContributorDictionary>();
			if(contributorDict != null)
			{
				if(!contributorDict.ContainsKey(repositoryName))
				{
					contributorDict.Add(repositoryName, new List<IRepositoryContributor>());
				}

				TContributor contributor = new TContributor();
				contributorDict[repositoryName].Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IRepositoryContributor));
			}

			return services;
		}

		/// <summary>
		///     Adds the given repository contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositoryProviderContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IRepositoryProviderContributor, new()
		{
			Guard.Against.Null(services);

			RepositoryProviderContributorList contributorList = services.GetObjectOrDefault<RepositoryProviderContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IRepositoryProviderContributor));
			}

			return services;
		}
	}
}
