namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	internal static class HealthCheckResponseWriter
	{
		private const string DefaultContentType = "application/json";

		public static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
		{
			return WriteHealthCheckResponse(httpContext, report, null);
		}

		private static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report, Action<JsonSerializerOptions> configureAction)
		{
			string response = string.Empty;

			if(report is not null)
			{
				JsonSerializerOptions settings = new JsonSerializerOptions
				{
					WriteIndented = false,
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
				};

				configureAction?.Invoke(settings);

				settings.Converters.Add(new JsonStringEnumConverter());

				httpContext.Response.ContentType = DefaultContentType;

				FormattedHealthReport healthReport = FormattedHealthReport.CreateFrom(report);

				response = JsonSerializer.Serialize(healthReport, settings);
			}

			return httpContext.Response.WriteAsync(response);
		}
	}
}
