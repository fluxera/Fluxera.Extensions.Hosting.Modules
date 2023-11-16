namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Json;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	/// <summary>
	///		Contains helper methods for reading and writing a formatted health report.
	/// </summary>
	public static class HealthCheckResponseWriter
	{
		private const string DefaultContentType = "application/json";

		private const string EmptyResponse = "{}";

		private static readonly Lazy<JsonSerializerOptions> Options = new Lazy<JsonSerializerOptions>(CreateJsonOptions);

		internal static async Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
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

		/// <summary>
		///		Reads the formatted health report from a HTTP response message.
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		public static async Task<FormattedHealthReport> ReadHealthReport(HttpResponseMessage response)
		{
			FormattedHealthReport healthReport = await response.Content.ReadFromJsonAsync<FormattedHealthReport>(Options.Value);
			return healthReport;
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
