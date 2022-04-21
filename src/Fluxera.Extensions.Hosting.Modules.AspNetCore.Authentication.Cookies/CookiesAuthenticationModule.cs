namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables JWT Bearer authentication.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AuthenticationModule))]
	public sealed class CookiesAuthenticationModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the configure authentication contributor.
			context.Services.AddAuthenticationContributor<AuthenticationContributor>();
		}
	}
}
