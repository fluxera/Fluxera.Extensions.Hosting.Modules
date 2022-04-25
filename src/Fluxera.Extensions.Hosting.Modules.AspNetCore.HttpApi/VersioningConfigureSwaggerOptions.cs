namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Asp.Versioning.ApiExplorer;
	using Microsoft.Extensions.Options;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	internal sealed class VersioningConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

		public VersioningConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
		{
			this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
		}

		public void Configure(SwaggerGenOptions swaggerGenOptions)
		{
			// Modify swagger document for every API version discovered.
			foreach(ApiVersionDescription description in this.apiVersionDescriptionProvider.ApiVersionDescriptions)
			{
				if(swaggerGenOptions.SwaggerGeneratorOptions.SwaggerDocs.TryGetValue(description.GroupName, out OpenApiInfo info))
				{
					if(description.IsDeprecated)
					{
						info.Description += "<br/><br/><strong>This API version has been deprecated.</strong>";
					}
				}
			}
		}

		public void Configure(string name, SwaggerGenOptions swaggerGenOptions)
		{
			this.Configure(swaggerGenOptions);
		}
	}
}
