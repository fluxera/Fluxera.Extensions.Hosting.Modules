namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Asp.Versioning.ApiExplorer;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	internal sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly HttpApiOptions options;
		private readonly IApiVersionDescriptionProvider versionDescriptionProvider;

		public ConfigureSwaggerOptions(
			IApiVersionDescriptionProvider versionDescriptionProvider,
			IOptions<HttpApiOptions> options)
		{
			this.versionDescriptionProvider = versionDescriptionProvider;
			this.options = options.Value;
		}

		public void Configure(SwaggerGenOptions swaggerGenOptions)
		{
			// Add swagger document for every API version discovered.
			foreach(ApiVersionDescription versionDescription in this.versionDescriptionProvider.ApiVersionDescriptions)
			{
				swaggerGenOptions.SwaggerDoc(versionDescription.GroupName, this.CreateVersionInfo(versionDescription.GroupName, versionDescription));
			}
		}

		public void Configure(string name, SwaggerGenOptions swaggerGenOptions)
		{
			this.Configure(swaggerGenOptions);
		}

		private OpenApiInfo CreateVersionInfo(string groupName, ApiVersionDescription versionDescription)
		{
			this.options.Descriptions.TryGetValue(groupName, out HttpApiDescription apiDescription);

			return apiDescription.CreateApiInfo(versionDescription);
		}
	}
}
