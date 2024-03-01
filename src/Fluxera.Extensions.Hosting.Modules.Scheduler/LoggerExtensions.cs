namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Information, "Global Subscription ID: {GlobalSubscriptionID} ({Environment}, {Application})")]
		public static partial void LogSchedulerIdentifier(this ILogger logger, string globalSubscriptionID, string environment, string application);
	}
}
