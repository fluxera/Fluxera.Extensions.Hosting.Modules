namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using MadEyeMatt.AspNetCore.Authentication.Basic;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			BasicAuthenticationOptions authenticationOptions = context.Services.GetOptions<BasicAuthenticationOptions>();

			context.Log("AddBasicUserAuthenticationServiceFactory",
				services => services.AddTransient<IBasicUserAuthenticationServiceFactory, BasicUserAuthenticationServiceFactory>());

			// Add all configures Basic schemes.
			foreach((string key, BasicAuthenticationSchemeOptions schemeOptions) in authenticationOptions.Schemes)
			{
				context.Log($"AddBasicAuthentication({key})", _ =>
				{
					string schemeName = key.CalculateSchemeName(BasicDefaults.AuthenticationScheme);

					builder.AddBasic(schemeName, options =>
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
