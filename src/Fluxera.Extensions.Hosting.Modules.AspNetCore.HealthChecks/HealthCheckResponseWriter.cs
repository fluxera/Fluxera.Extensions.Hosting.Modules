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

		private const string EmptyResponse = "{}";

		private static readonly Lazy<JsonSerializerOptions> Options = new Lazy<JsonSerializerOptions>(CreateJsonOptions);

		public static async Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
		{
			httpContext.Response.ContentType = DefaultContentType;

			if(report is not null)
			{
				FormattedHealthReport healthReport = FormattedHealthReport.CreateFrom(report);

				await JsonSerializer.SerializeAsync(httpContext.Response.Body, healthReport, Options.Value);
			}
			else
			{
				await httpContext.Response.WriteAsync(EmptyResponse);
			}
		}

		private static JsonSerializerOptions CreateJsonOptions()
		{
			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = false,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
			};

			options.Converters.Add(new JsonStringEnumConverter());

			return options;
		}
	}
}
