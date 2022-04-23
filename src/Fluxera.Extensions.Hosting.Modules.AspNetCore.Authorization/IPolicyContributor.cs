namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contributor for providing authorization policies.
	/// </summary>
	[PublicAPI]
	public interface IPolicyContributor
	{
		/// <summary>
		///     Add the policy to the options.
		/// </summary>
		/// <param name="options"></param>
		void AddPolicy(AuthorizationOptions options);

		/// <summary>
		///     Add the policy handlers to the services.
		/// </summary>
		/// <param name="services"></param>
		void AddPolicyHandlers(IServiceCollection services);
	}
}
