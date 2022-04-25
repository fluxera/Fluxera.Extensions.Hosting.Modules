namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Versioning
{
	using Asp.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Versioning.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Swashbuckle.AspNetCore.SwaggerGen;

	/// <summary>
	///     A module that enables API versioning.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(HttpApiModule))]
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
				services
					.AddApiVersioning(options =>
					{
						options.DefaultApiVersion = new ApiVersion(versioningOptions.DefaultApiVersion.Major, versioningOptions.DefaultApiVersion.Major);
						options.AssumeDefaultVersionWhenUnspecified = true;
						options.ReportApiVersions = true;
					})
					.AddMvc()
					.AddApiExplorer(options =>
					{
						options.GroupNameFormat = "'v'VVV";
						options.SubstituteApiVersionInUrl = true;
					});
			});

			// Configure additional operation filters.
			context.Services.Configure<SwaggerGenOptions>(options =>
			{
				options.OperationFilter<DefaultValuesFilter>();
				options.OperationFilter<DeprecatedOperationFilter>();
			});
		}
	}
}
