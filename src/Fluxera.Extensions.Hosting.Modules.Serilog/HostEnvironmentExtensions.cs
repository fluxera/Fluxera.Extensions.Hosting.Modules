namespace Fluxera.Extensions.Hosting.Modules.Serilog
{
	using System;
	using System.IO;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Hosting;

	/// <summary>
	///     Extensions methods for the <see cref="IHostEnvironment" /> type.
	/// </summary>
	[PublicAPI]
	public static class HostEnvironmentExtensions
	{
		/// <summary>
		///     Gets the default log file path for Serilog.
		/// </summary>
		/// <param name="environment"></param>
		/// <returns></returns>
		public static string GetDefaultLogFilePath(this IHostEnvironment environment)
		{
			string baseDirectory = AppContext.BaseDirectory;
			string logFilePath = Path.Combine(baseDirectory, "logs", environment.ApplicationName + "_.log");
			return logFilePath;
		}
	}
}
