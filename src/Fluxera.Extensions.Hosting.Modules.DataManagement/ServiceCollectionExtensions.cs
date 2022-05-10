namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

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
		public static IServiceCollection AddDataSeedingContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IDataSeedingContributor, new()
		{
			IHostEnvironment environment = services.GetObject<IHostEnvironment>();

			// We only support the data seeders in non-production environments.
			if(!environment.IsProduction())
			{
				DataSeedingContributorList contributorList = services.GetObjectOrDefault<DataSeedingContributorList>();
				if(contributorList != null)
				{
					TContributor contributor = new TContributor();
					contributorList.Add(contributor);
				}
				else
				{
					ILogger logger = services.GetObjectOrDefault<ILogger>();
					logger?.LogWarning("The contributor list for {Contributor} was not available.", typeof(IDataSeedingContributor));
				}
			}

			return services;
		}
	}
}
