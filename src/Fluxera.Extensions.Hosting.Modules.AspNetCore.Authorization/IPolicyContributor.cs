namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;

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
		/// <param name="context"></param>
		void AddPolicy(AuthorizationOptions options, IServiceConfigurationContext context);

		/// <summary>
		///     Add the policy handlers to the services.
		/// </summary>
		/// <param name="context"></param>
		void AddPolicyHandlers(IServiceConfigurationContext context);
	}
}
