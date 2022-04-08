namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

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
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDataSeedingContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IDataSeedingContributor, new()
		{
			IHostEnvironment environment = services.GetObject<IHostEnvironment>();

			// We only support the data seeders in non-production environments.
			if(!environment.IsProduction())
			{
				DataSeedingContributorList contributors = services.GetObject<DataSeedingContributorList>();
				TContributor contributor = new TContributor();
				contributors.Add(contributor);
			}

			return services;
		}
	}
}
