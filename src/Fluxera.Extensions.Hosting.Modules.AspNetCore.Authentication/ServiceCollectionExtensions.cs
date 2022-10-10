namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
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
		///     Adds the specified <see cref="IAuthenticationContributor" /> for the module.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddAuthenticationContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IAuthenticationContributor, new()
		{
			Guard.Against.Null(services);

			AuthenticationContributorList contributorList = services.GetObjectOrDefault<AuthenticationContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IAuthenticationContributor));
			}

			return services;
		}
	}
}
