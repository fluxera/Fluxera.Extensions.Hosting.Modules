namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox
{
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Warning, "The in-memory inbox/outbox is configured. Use only in non-production environments.")]
		public static partial void LogInMemoryInboxOutboxUsed(this ILogger logger);

		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Information, "The EFCore inbox/outbox is configured.")]
		public static partial void LogEntityFrameworkCoreInboxOutboxUsed(this ILogger logger);
	}
}
