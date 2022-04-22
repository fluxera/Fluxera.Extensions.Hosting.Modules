namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			CookiesAuthenticationOptions authenticationOptions = context.Services.GetOptions<CookiesAuthenticationOptions>();

			// Add all configures Cookies schemes.
			foreach((string key, CookiesAuthenticationSchemeOptions schemeOptions) in authenticationOptions.Schemes)
			{
				context.Log($"AddCookiesAuthentication({key})", _ =>
				{
					string schemeName = key.CalculateSchemeName(CookieAuthenticationDefaults.AuthenticationScheme);

					builder.AddCookie(schemeName, options =>
					{
						options.Cookie.Name = schemeOptions.Cookie.Name;
						options.AccessDeniedPath = schemeOptions.AccessDeniedPath;
						options.LoginPath = schemeOptions.LoginPath;
						options.LogoutPath = schemeOptions.LogoutPath;
						options.SlidingExpiration = schemeOptions.SlidingExpiration;
						options.ExpireTimeSpan = schemeOptions.ExpireTimeSpan;
					});
				});
			}
		}
	}
}
