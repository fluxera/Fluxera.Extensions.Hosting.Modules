namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Debug, "Found duplicate request, acquired data from cache: {IdempotentToken}.")]
		public static partial void LogFoundDuplicateRequest(this ILogger logger, string idempotentToken);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Debug, "Storing response data in cache: {IdempotentToken}")]
		public static partial void LogStoringResponseData(this ILogger logger, string idempotentToken);
	}
}
