namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Asp.Versioning;
	using Asp.Versioning.ApiExplorer;
	using Microsoft.OpenApi.Models;

	internal static class HttpApiDescriptionExtensions
	{
		public static OpenApiInfo CreateApiInfo(this HttpApiDescription apiDescription, ApiVersionDescription versionDescription = null)
		{
			OpenApiInfo info = new OpenApiInfo
			{
				Version = versionDescription?.ApiVersion.ToString() ?? ApiVersion.Default.ToString(),
				Title = apiDescription?.Title ?? "The API title.",
				Description = apiDescription?.Description ?? "The API description."
			};

			if(versionDescription?.IsDeprecated == true)
			{
				info.Description += "<br/><br/><strong>This API version has been deprecated.</strong>";
			}

			// TODO: Extend with further options if needed.

			return info;
		}
	}
}
