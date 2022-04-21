namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using global::AspNetCore.Authentication.ApiKey;
	using Microsoft.AspNetCore.Authentication;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			ApiKeyAuthenticationOptions authenticationOptions = context.Services.GetOptions<ApiKeyAuthenticationOptions>();

			// Add all configures ApiKey schemes.
			foreach((string key, ApiKeyAuthenticationSchemeOptions schemeOptions) in authenticationOptions.ApiKey)
			{
				context.Log($"AddApiKeyAuthentication({key})", _ =>
				{
					builder.AddApiKeyInHeader<ApiKeyProvider>(key, options =>
					{
						if(schemeOptions.KeyName.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The KeyName configuration value must be set.");
						}

						if(schemeOptions.Realm.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The Realm configuration value must be set.");
						}

						options.KeyName = schemeOptions.KeyName;
						options.Realm = schemeOptions.Realm;
						options.IgnoreAuthenticationIfAllowAnonymous = schemeOptions.IgnoreAuthenticationIfAllowAnonymous;
						options.SuppressWWWAuthenticateHeader = schemeOptions.SuppressWWWAuthenticateHeader;
					});
				});
			}
		}
	}
}
