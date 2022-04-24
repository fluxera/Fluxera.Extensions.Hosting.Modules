namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors
{
	using System;
	using System.Linq;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables CORS.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class CorsModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddCors", services =>
			{
				CorsOptions corsOptions = services.GetOptions<CorsOptions>();

				// TODO: Make everything configurable. May add multiple policies.
				services.AddCors(options =>
				{
					options.AddPolicy(CorsOptions.DefaultCorsPolicyName, builder =>
					{
						string[] origins = corsOptions.Origins?
							.Select(o => o.RemovePostFix("/"))
							.ToArray() ?? Array.Empty<string>();

						builder.WithOrigins(origins)
							.SetIsOriginAllowedToAllowWildcardSubdomains()
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
					});
				});
			});
		}
	}
}
