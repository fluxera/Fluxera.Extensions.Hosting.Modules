﻿namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Versioning
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Versioning.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables API versioning.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class VersioningModule : ConfigureServicesModule
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
			VersioningOptions versioningOptions = context.Services.GetOptions<VersioningOptions>();

			// Add API versioning.
			context.Log("AddApiVersioning", services =>
			{
				// https://www.hanselman.com/blog/ASPNETCoreRESTfulWebAPIVersioningMadeEasy.aspx
				// https://github.com/Microsoft/aspnet-api-versioning/wiki
				services.AddApiVersioning(options =>
				{
					options.DefaultApiVersion = new ApiVersion(versioningOptions.DefaultApiVersion.Major, versioningOptions.DefaultApiVersion.Major);
					options.AssumeDefaultVersionWhenUnspecified = true;
					options.UseApiBehavior = false;
					options.ReportApiVersions = true;
				});
			});

			// Add the versioned API explorer.
			context.Log("AddVersionedApiExplorer", services =>
			{
				services.AddVersionedApiExplorer(setup =>
				{
					setup.GroupNameFormat = "'v'VVV";
					setup.SubstituteApiVersionInUrl = true;
				});
			});
		}
	}
}