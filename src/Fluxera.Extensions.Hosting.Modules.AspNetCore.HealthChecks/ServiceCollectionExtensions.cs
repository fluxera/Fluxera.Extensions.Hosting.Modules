namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
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
		///     Add the health check contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddHealthCheckContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IHealthCheckContributor, new()
		{
			HealthCheckContributorList healthCheckContributorList = services.GetObject<HealthCheckContributorList>();
			TContributor contributor = new TContributor();
			healthCheckContributorList.Add(contributor);

			return services;
		}
	}
}
