namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System;
	using System.Diagnostics;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extension methods for the <see cref="ILogger" /> type.
	/// </summary>
	[PublicAPI]
	public static partial class LoggerExtensions
	{
		/// <summary>
		///     Logs a warning that a specialized list for the given contributor type was not available
		///     when tying to add a contributor to it.
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="contributor"></param>
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Warning, "The contributor list for {Contributor} was not available.")]
		public static partial void LogContributorListNotAvailable(this ILogger logger, Type contributor);
	}
}
