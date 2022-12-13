namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Contains the service collection extensions of the module.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the specified <see cref="IConfigureOptionsContributor" /> for the module.
		/// </summary>
		/// <remarks>
		///     The contributors must be added in the pre configure services calls.
		/// </remarks>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddConfigureOptionsContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IConfigureOptionsContributor, new()
		{
			Guard.Against.Null(services);

			ConfigureContributorList contributorList = services.GetObjectOrDefault<ConfigureContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IConfigureOptionsContributor));
			}

			return services;
		}

		/// <summary>
		///     Gets the options instance from the added <see cref="IObjectAccessor" /> or
		///     creates a new instance if the options are not available in the service
		///     collection.
		/// </summary>
		/// <typeparam name="TOptions"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static TOptions GetOptions<TOptions>(this IServiceCollection services)
			where TOptions : class, new()
		{
			Guard.Against.Null(services);

			return services.GetObjectOrDefault<TOptions>() ?? new TOptions();
		}
	}
}
