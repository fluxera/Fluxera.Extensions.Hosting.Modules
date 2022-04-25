namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using Microsoft.Extensions.Options;
	using Swashbuckle.AspNetCore.SwaggerGen;

	internal sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly SwaggerOptions options;

		public ConfigureSwaggerOptions(IOptions<SwaggerOptions> options)
		{
			this.options = options.Value;
		}

		public void Configure(SwaggerGenOptions swaggerGenOptions)
		{
			//// Add swagger document for every API version discovered.
			//foreach ((string groupName, HttpApiDescription description) in this.options.Descriptions)
			//{
			//	swaggerGenOptions.SwaggerDoc(groupName, CreateVersionInfo(description));
			//}
		}

		public void Configure(string name, SwaggerGenOptions swaggerGenOptions)
		{
			this.Configure(swaggerGenOptions);
		}

		//private static OpenApiInfo CreateVersionInfo(HttpApiDescription description)
		//{
		//	OpenApiInfo info = new OpenApiInfo
		//	{
		//		Version = description?.Version.ToString() ?? "1.0",
		//		Title = description?.Title ?? string.Empty,
		//		Description = description?.Description ?? string.Empty,

		//		// TODO: Extend options if needed.
		//		//Contact = new OpenApiContact
		//		//{
		//		//	Url = new Uri(""),
		//		//	Email = "",
		//		//	Name = ""
		//		//},
		//		//License = new OpenApiLicense
		//		//{
		//		//	Url = new Uri(""),
		//		//	Name = ""
		//		//},
		//		//TermsOfService = new Uri(""),
		//		//Extensions =
		//		//{
		//		//}
		//	};

		//	//if(description.IsDeprecated)
		//	//{
		//	//	info.Description += "<br/><br/><strong>This API version has been deprecated.</strong>";
		//	//}

		//	return info;
		//}
	}
}
