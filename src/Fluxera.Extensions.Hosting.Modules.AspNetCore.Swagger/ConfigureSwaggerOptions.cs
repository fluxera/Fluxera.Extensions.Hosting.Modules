namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using Microsoft.AspNetCore.Mvc.ApiExplorer;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	internal sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly SwaggerOptions options;
		private readonly IApiVersionDescriptionProvider versionDescriptionProvider;

		public ConfigureSwaggerOptions(
			IOptions<SwaggerOptions> options,
			IApiVersionDescriptionProvider versionDescriptionProvider)
		{
			this.options = options.Value;
			this.versionDescriptionProvider = versionDescriptionProvider;
		}

		public void Configure(SwaggerGenOptions swaggerGenOptions)
		{
			// Add swagger document for every API version discovered.
			foreach(ApiVersionDescription description in this.versionDescriptionProvider.ApiVersionDescriptions)
			{
				swaggerGenOptions.SwaggerDoc(description.GroupName, this.CreateVersionInfo(description));
			}
		}

		public void Configure(string name, SwaggerGenOptions swaggerGenOptions)
		{
			this.Configure(swaggerGenOptions);
		}

		private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
		{
			this.options.Descriptions.TryGetValue(description.GroupName, out SwaggerApiDescription swaggerApiDescription);

			OpenApiInfo info = new OpenApiInfo
			{
				Version = description.ApiVersion.ToString(),
				Title = swaggerApiDescription?.Title ?? string.Empty,
				Description = swaggerApiDescription?.Description ?? string.Empty,

				// TODO: Extend options if needed.
				//Contact = new OpenApiContact
				//{
				//	Url = new Uri(""),
				//	Email = "",
				//	Name = ""
				//},
				//License = new OpenApiLicense
				//{
				//	Url = new Uri(""),
				//	Name = ""
				//},
				//TermsOfService = new Uri(""),
				//Extensions =
				//{
				//}
			};

			if(description.IsDeprecated)
			{
				info.Description += "<br/><br/><strong>This API version has been deprecated.</strong>";
			}


			return info;
		}
	}
}
