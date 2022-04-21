namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using System;
	using Microsoft.AspNetCore.Authentication;

	internal static class AuthenticationBuilderExtensions
	{
		public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, Action<BasicSchemeOptions> configureAction)
		{
			return builder.AddScheme<BasicSchemeOptions, BasicAuthenticationHandler>(BasicDefaults.AuthenticationScheme, configureAction);
		}
	}
}
