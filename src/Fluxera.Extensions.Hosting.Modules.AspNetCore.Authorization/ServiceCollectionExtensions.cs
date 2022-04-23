namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
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
		///     Adds the authorize contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddAuthorizeContributor<T>(this IServiceCollection services)
			where T : class, IAuthorizeContributor, new()
		{
			AuthorizeContributorList authorizeContributorList = services.GetObject<AuthorizeContributorList>();
			authorizeContributorList.Add(new T());

			return services;
		}

		//public static IServiceCollection AddPolicyContributor<T>(this IServiceCollection services)
		//	where T : class, IPolicyContributor, new()
		//{
		//	PolicyContributorList policyContributorList = services.GetObject<PolicyContributorList>();
		//	policyContributorList.Add(new T());

		//	return services;
		//}
	}
}
