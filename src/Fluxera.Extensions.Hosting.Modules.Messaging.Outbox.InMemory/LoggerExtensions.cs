namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.InMemory
{
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Warning, "The in-memory inbox/outbox is configured. Use only in non-production environments.")]
		public static partial void LogInMemoryInboxOutboxUsed(this ILogger logger);
	}
}
