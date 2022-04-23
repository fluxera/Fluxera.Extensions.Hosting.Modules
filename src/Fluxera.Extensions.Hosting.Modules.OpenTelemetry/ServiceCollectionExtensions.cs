namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Contains the service collection extensions of the module.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the specified <see cref="ITracerProviderContributor" /> for the module.
		/// </summary>
		/// <remarks>
		///     The contributors must be added in the pre configure services calls.
		/// </remarks>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddTracerProviderContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, ITracerProviderContributor, new()
		{
			Guard.Against.Null(services, nameof(services));

			TracerProviderContributorList contributorList = services.GetObject<TracerProviderContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}

		/// <summary>
		///     Adds the specified <see cref="IMeterProviderContributor" /> for the module.
		/// </summary>
		/// <remarks>
		///     The contributors must be added in the pre configure services calls.
		/// </remarks>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMeterProviderContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IMeterProviderContributor, new()
		{
			Guard.Against.Null(services, nameof(services));

			MeterProviderContributorList contributorList = services.GetObject<MeterProviderContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}
	}
}
