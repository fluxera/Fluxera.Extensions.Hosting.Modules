namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
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
		///     Adds the specified <see cref="IAuthenticationContributor" /> for the module.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddAuthenticationContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IAuthenticationContributor, new()
		{
			Guard.Against.Null(services, nameof(services));

			AuthenticationContributorList contributorList = services.GetObject<AuthenticationContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}
	}
}
