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
		public static IHostBuilder AddSerilogLogging(this IHostBuilder builder, Action<HostBuilderContext, LoggerConfiguration> configureAction)
		{
			return builder.UseSerilog((context, services, options) =>
			{
				options
					.Enrich.FromLogContext()
					.Enrich.WithEnvironmentName()
					.Enrich.WithEnvironmentUserName()
					.Enrich.WithMachineName()
					.Enrich.WithThreadId()
					.Enrich.WithThreadName()
					.Enrich.WithProcessId()
					.Enrich.WithProcessName()
					.Enrich.WithAssemblyName()
					.Enrich.WithAssemblyVersion()
					.Enrich.WithAssemblyInformationalVersion()
					.ReadFrom.Configuration(context.Configuration)
					.ReadFrom.Services(services);

				configureAction?.Invoke(context, options);
			});
		}

		/// <summary>
		///     Adds the Serilog logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="configureAction">Configure additional options.</param>
		/// <returns></returns>
		public static IHostBuilder AddSerilogLogging(this IHostBuilder builder, Action<LoggerConfiguration> configureAction)
		{
			return builder.AddSerilogLogging((_, options) =>
			{
				configureAction?.Invoke(options);
			});
		}

		/// <summary>
		///     Adds the Serilog logging.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IHostBuilder AddSerilogLogging(this IHostBuilder builder)
		{
			return builder.AddSerilogLogging((_, _) =>
			{
			});
		}
	}
}
