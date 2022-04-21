namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using global::AspNetCore.Authentication.ApiKey;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables JWT Bearer authentication.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AuthenticationModule))]
	public sealed class BasicAuthenticationModule : ConfigureServicesModule
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
			context.Log("AddApiKeyAuthentication", services =>
			{
				ApiKeyAuthenticationOptions authenticationOptions = services.GetOptions<ApiKeyAuthenticationOptions>();

				services
					.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
					.AddApiKeyInHeader<ApiKeyProvider>(options =>
					{
						if(authenticationOptions.Realm.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The Realm configuration value must be set.");
						}

						options.KeyName = authenticationOptions.KeyName;
						options.Realm = authenticationOptions.Realm;

						options.IgnoreAuthenticationIfAllowAnonymous = authenticationOptions.IgnoreAuthenticationIfAllowAnonymous;
						options.SuppressWWWAuthenticateHeader = authenticationOptions.SuppressWWWAuthenticateHeader;
					});
			});
		}
	}
}
