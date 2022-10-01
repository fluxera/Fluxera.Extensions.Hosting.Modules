namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System;
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Information, "Initializing endpoints.")]
		public static partial void LogInitializingEndpoints(this ILogger logger);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Information, "Initializing services dependencies.")]
		public static partial void LogInitializingServicesDependencies(this ILogger logger);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Debug, "Initializing endpoint: {Controller}.{Action}")]
		public static partial void LogInitializingEndpoint(this ILogger logger, string controller, string action);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Debug, "Initializing service dependency: {Service}")]
		public static partial void LogInitializingServicesDependency(this ILogger logger, Type service);
	}
}
