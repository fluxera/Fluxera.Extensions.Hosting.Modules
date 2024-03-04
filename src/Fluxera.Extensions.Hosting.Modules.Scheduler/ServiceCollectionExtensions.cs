namespace Fluxera.Extensions.Hosting.Modules.Scheduler
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
		///     Adds the store contributor if no other store contributor was already added.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddStoreContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IStoreContributor, new()
		{
			TContributor contributor = new TContributor();
			services.TryAddObjectAccessor<IStoreContributor>(contributor);

			return services;
		}

		/// <summary>
		///     Adds a scheduler contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddSchedulerContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, ISchedulerContributor, new()
		{
			SchedulerContributorList contributorList = services.GetObjectOrDefault<SchedulerContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(ISchedulerContributor));
			}

			return services;
		}
	}
}
