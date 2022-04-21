namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			CookiesAuthenticationOptions authenticationOptions = context.Services.GetOptions<CookiesAuthenticationOptions>();

			// Add all configures ApiKey schemes.
			foreach((string key, CookiesAuthenticationSchemeOptions schemeOptions) in authenticationOptions.Cookies)
			{
				context.Log($"AddCookiesAuthentication({key})", _ =>
				{
					builder.AddCookie(key, options =>
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
