namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry
{
	using System;
	using global::OpenTelemetry.Logs;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extensions methods for the <see cref="IHostBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class HostBuilderExtensions
	{
		/// <summary>
		///     Adds the OpenTelemetry logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="configureAction">Configure additional options.</param>
		/// <returns></returns>
		public static IHostBuilder AddOpenTelemetryLogging(this IHostBuilder builder, Action<HostBuilderContext, OpenTelemetryLoggerOptions> configureAction)
		{
			return builder.ConfigureLogging((context, loggingBuilder) =>
			{
				IConfigurationSection section = context.Configuration.GetSection("OpenTelemetry");
				OpenTelemetryOptions telemetryOptions = section.Get<OpenTelemetryOptions>() ?? new OpenTelemetryOptions();

				loggingBuilder.AddOpenTelemetry(loggerOptions =>
				{
					loggerOptions.IncludeFormattedMessage = true;
					loggerOptions.IncludeScopes = true;
					loggerOptions.ParseStateValues = true;

					loggerOptions.AddOtlpExporter(options => options.Endpoint = new Uri(telemetryOptions.OpenTelemetryProtocolEndpoint));

					configureAction?.Invoke(context, loggerOptions);
				});
			});
		}

		/// <summary>
		///     Adds the OpenTelemetry logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="configureAction">Configure additional options.</param>
		/// <returns></returns>
		public static IHostBuilder AddOpenTelemetryLogging(this IHostBuilder builder, Action<OpenTelemetryLoggerOptions> configureAction)
		{
			return builder.AddOpenTelemetryLogging((_, options) =>
			{
				configureAction?.Invoke(options);
			});
		}

		/// <summary>
		///     Adds the OpenTelemetry logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IHostBuilder AddOpenTelemetryLogging(this IHostBuilder builder)
		{
			return builder.AddOpenTelemetryLogging((_, _) =>
			{
			});
		}
	}
}
