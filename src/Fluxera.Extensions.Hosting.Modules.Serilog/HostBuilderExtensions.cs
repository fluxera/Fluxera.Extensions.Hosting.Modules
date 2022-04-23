namespace Fluxera.Extensions.Hosting.Modules.Serilog
{
	using System;
	using global::Serilog;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Hosting;

	/// <summary>
	///     Extensions methods for the <see cref="IHostBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class HostBuilderExtensions
	{
		/// <summary>
		///     Adds the Serilog logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="configureAction">Configure additional options.</param>
		/// <returns></returns>
		public static IHostBuilder AddSerilogLogging(this IHostBuilder builder, Action<HostBuilderContext, LoggerConfiguration> configureAction = null)
		{
			return builder.UseSerilog((hostingContext, services, loggerOptions) =>
			{
				loggerOptions
					.Enrich.FromLogContext()
					.ReadFrom.Configuration(hostingContext.Configuration)
					.ReadFrom.Services(services);

				configureAction?.Invoke(hostingContext, loggerOptions);
			});
		}

		/// <summary>
		///     Adds the Serilog logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="configureAction">Configure additional options.</param>
		/// <returns></returns>
		public static IHostBuilder AddSerilogLogging(this IHostBuilder builder, Action<LoggerConfiguration> configureAction = null)
		{
			return builder.AddSerilogLogging((_, loggerOptions) =>
			{
				configureAction?.Invoke(loggerOptions);
			});
		}
	}
}
