namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Versioning
{
	using Asp.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Versioning.Contributors;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Swagger;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Versioning;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables versioning for OData.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(SwaggerModule))]
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
			SwaggerOptions swaggerOptions = context.Services.GetOptions<SwaggerOptions>();
			VersioningOptions versioningOptions = context.Services.GetOptions<VersioningOptions>();

			// Add OData API versioning.
			context.Log("AddApiVersioning", services =>
			{
				// https://github.com/dotnet/aspnet-api-versioning/wiki
				IApiVersioningBuilder versioningBuilder = services.AddApiVersioning(options =>
				{
					options.DefaultApiVersion = new ApiVersion(versioningOptions.DefaultApiVersion.Major, versioningOptions.DefaultApiVersion.Major);
					options.AssumeDefaultVersionWhenUnspecified = true;
					options.ReportApiVersions = true;
				});

				versioningBuilder.AddOData(options =>
				{
					options.AddRouteComponents("api/v{version:apiVersion}");
				});

				if(swaggerOptions.Enabled)
				{
					versioningBuilder.AddApiExplorer(options =>
					{
						options.GroupNameFormat = "'v'VVV";
						options.SubstituteApiVersionInUrl = true;
					});
				}
			});

			//context.Services.TryAddTransient<VersionedMetadataController>();
		}
	}
}
