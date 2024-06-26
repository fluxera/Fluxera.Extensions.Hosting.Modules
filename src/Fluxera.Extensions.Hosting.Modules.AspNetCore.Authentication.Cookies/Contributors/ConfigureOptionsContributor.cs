﻿namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<CookiesAuthenticationOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Authentication:Schemes";
	}
}
