namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Error, "The message authentication failed.")]
		public static partial void LogMessageAuthenticationFailed(this ILogger logger, Exception exception);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Error, "The message validation failed.")]
		public static partial void LogMessageValidationFailed(this ILogger logger, Exception exception);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Error, "The principal creation failed.")]
		public static partial void LogPrincipalCreationFailed(this ILogger logger, Exception exception);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Information, "Global Subscription ID: {GlobalSubscriptionID} ({Environment}, {Application})")]
		public static partial void LogServiceSubscriptionInfo(this ILogger logger, string globalSubscriptionID, string environment, string application);
	}
}
