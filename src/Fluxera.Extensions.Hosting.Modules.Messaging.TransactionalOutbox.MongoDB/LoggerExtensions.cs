namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.MongoDB
{
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Warning, "The MongoDB inbox/outbox is configured.")]
		public static partial void LogMongoDbInboxOutboxUsed(this ILogger logger);
	}
}
