namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using global::AspNetCore.Authentication.Basic;
	using Microsoft.AspNetCore.Authentication;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			BasicAuthenticationOptions authenticationOptions = context.Services.GetOptions<BasicAuthenticationOptions>();

			// Add all configures Basic schemes.
			foreach((string key, BasicAuthenticationSchemeOptions schemeOptions) in authenticationOptions.Basic)
			{
				context.Log($"AddBasicAuthentication({key})", _ =>
				{
					string schemeName = key;
					if(schemeName == BasicAuthenticationSchemes.DefaultSchemeName)
					{
						schemeName = BasicDefaults.AuthenticationScheme;
					}

					builder.AddBasic<BasicUserValidationService>(schemeName, options =>
					{
						if(schemeOptions.Realm.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The Realm configuration value must be set.");
						}

						options.Realm = schemeOptions.Realm;
						options.IgnoreAuthenticationIfAllowAnonymous = schemeOptions.IgnoreAuthenticationIfAllowAnonymous;
						options.SuppressWWWAuthenticateHeader = schemeOptions.SuppressWWWAuthenticateHeader;
					});
				});
			}
		}
	}
}
