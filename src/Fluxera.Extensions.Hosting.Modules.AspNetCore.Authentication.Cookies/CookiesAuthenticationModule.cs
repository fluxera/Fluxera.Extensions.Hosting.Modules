namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.Extensions.DependencyInjection;

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
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddBasicAuthentication", services =>
			{
				CookiesAuthenticationOptions authenticationOptions = services.GetOptions<CookiesAuthenticationOptions>();

				services
					.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie(options =>
					{
						options.Cookie.Name = authenticationOptions.Cookie.Name;
						options.AccessDeniedPath = authenticationOptions.AccessDeniedPath;
						options.LoginPath = authenticationOptions.LoginPath;
						options.LogoutPath = authenticationOptions.LogoutPath;
						options.SlidingExpiration = authenticationOptions.SlidingExpiration;
						options.ExpireTimeSpan = authenticationOptions.ExpireTimeSpan;
					});
			});
		}
	}
}
