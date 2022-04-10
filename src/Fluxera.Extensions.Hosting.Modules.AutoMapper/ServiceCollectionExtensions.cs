namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
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
		///     Adds the specified <see cref="IMappingProfileContributor" /> for the module.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMappingProfileContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IMappingProfileContributor, new()
		{
			Guard.Against.Null(services, nameof(services));

			MappingProfileContributorList contributorList = services.GetObject<MappingProfileContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}
	}
}
