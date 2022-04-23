namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;

	/// <summary>
	///     A contract for a contributor that configures authentication schemes.
	/// </summary>
	[PublicAPI]
	public interface IAuthenticationContributor
	{
		/// <summary>
		///     Configures authentication schemes on the given authentication builder.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context);
	}
}
