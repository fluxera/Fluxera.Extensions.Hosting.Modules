namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Contains the service collection extensions of the module.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the specified <see cref="IConfigureContributor" /> for the module.
		/// </summary>
		/// <remarks>
		///     The contributors must be added before the post configure services calls.
		/// </remarks>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddConfigureContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IConfigureContributor, new()
		{
			ConfigureContributorList contributorList = services.GetObject<ConfigureContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}
	}
}
