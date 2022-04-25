namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	internal sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly HttpApiOptions options;

		public ConfigureSwaggerOptions(IOptions<HttpApiOptions> options)
		{
			this.options = options.Value;
		}

		public void Configure(SwaggerGenOptions swaggerGenOptions)
		{
			// Add swagger document for every API version discovered.
			foreach((string groupName, HttpApiDescription description) in this.options.Descriptions)
			{
				swaggerGenOptions.SwaggerDoc(groupName, CreateVersionInfo(description));
			}
		}

		public void Configure(string name, SwaggerGenOptions swaggerGenOptions)
		{
			this.Configure(swaggerGenOptions);
		}

		private static OpenApiInfo CreateVersionInfo(HttpApiDescription description)
		{
			Version version = description.Version;

			OpenApiInfo info = new OpenApiInfo
			{
				Version = version.ToString(2),
				Title = $"{description.Title} V{version.Major}",
				Description = description.Description,

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

			//if(description.IsDeprecated)
			//{
			//	info.Description += "<br/><br/><strong>This API version has been deprecated.</strong>";
			//}

			return info;
		}
	}
}
