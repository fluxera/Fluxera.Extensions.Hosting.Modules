namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using MadEyeMatt.AspNetCore.Authentication.ApiKey;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			ApiKeyAuthenticationOptions authenticationOptions = context.Services.GetOptions<ApiKeyAuthenticationOptions>();

			context.Log("AddApiKeyAuthenticationServiceFactory",
				services => services.AddTransient<IApiKeyAuthenticationServiceFactory, ApiKeyAuthenticationServiceFactory>());

			// Add all configures ApiKey schemes.
			foreach((string key, ApiKeyAuthenticationSchemeOptions schemeOptions) in authenticationOptions.Schemes)
			{
				context.Log($"AddApiKeyAuthentication({key})", _ =>
				{
					string schemeName = key.CalculateSchemeName(ApiKeyDefaults.AuthenticationScheme);

					Func<string, Action<ApiKeyOptions>, AuthenticationBuilder> builderFunc = schemeOptions.Mode switch
					{
						ApiKeySchemeMode.KeyInHeader => builder.AddApiKeyInHeader,
						ApiKeySchemeMode.ApiKeyInAuthorizationHeader => builder.AddApiKeyInAuthorizationHeader,
						ApiKeySchemeMode.ApiKeyInQueryParams => builder.AddApiKeyInQueryParams,
						ApiKeySchemeMode.ApiKeyInHeaderOrQueryParams => builder.AddApiKeyInHeaderOrQueryParams,
						_ => throw new ArgumentOutOfRangeException(nameof(schemeOptions.Mode), "Unknown ApiKey scheme mode.")
					};

					builderFunc(schemeName, options =>
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
