namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Versioning
{
	using Asp.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Versioning.Contributors;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Versioning;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables versioning for OData.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(VersioningModule))]
	[DependsOn(typeof(ODataModule))]
	public class ODataVersioningModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the authorize contributor.
			context.Services.AddAuthorizeContributor<AuthorizeContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			VersioningOptions versioningOptions = context.Services.GetOptions<VersioningOptions>();

			// Add OData API versioning.
			context.Log("AddApiVersioning", services =>
			{
				// https://github.com/dotnet/aspnet-api-versioning/wiki
				services
					.AddApiVersioning(options =>
					{
						options.DefaultApiVersion = new ApiVersion(versioningOptions.DefaultApiVersion.Major, versioningOptions.DefaultApiVersion.Major);
						options.AssumeDefaultVersionWhenUnspecified = true;
						options.ReportApiVersions = true;
					})
					.AddOData(options =>
					{
						options.AddRouteComponents("api/v{version:apiVersion}");
					})
					.AddApiExplorer(options =>
					{
						options.GroupNameFormat = "'v'VVV";
						options.SubstituteApiVersionInUrl = true;
					});
			});

			//context.Services.TryAddTransient<VersionedMetadataController>();
		}
	}
}
