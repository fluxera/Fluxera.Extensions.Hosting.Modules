namespace Fluxera.Extensions.Hosting.Modules.MediatR
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
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
		///     Adds the specified <see cref="IMediatrContributor" /> for the module.
		/// </summary>
		/// <remarks>
		///     The contributors must be added in the pre-configure services calls.
		/// </remarks>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMediatrContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IMediatrContributor, new()
		{
			Guard.Against.Null(services);

			MediatrContributorList contributorList = services.GetObjectOrDefault<MediatrContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IMediatrContributor));
			}

			return services;
		}
	}
}
