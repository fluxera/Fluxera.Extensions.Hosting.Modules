﻿namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Principal
{
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables an accessor for the current user from the HTTP context.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(PrincipalModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class AspNetCorePrincipalModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the http context as source for a principal.
			context.Log("AddPrincipalProvider(HttpContext)",
				services => services.AddPrincipalProvider<HttpContextPrincipalProvider>());
		}
	}
}
