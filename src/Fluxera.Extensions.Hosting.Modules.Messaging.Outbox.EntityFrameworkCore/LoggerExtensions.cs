namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore
{
	using System.Diagnostics;
	using Microsoft.Extensions.Logging;

	internal static partial class LoggerExtensions
	{
		[DebuggerStepThrough]
		[LoggerMessage(0, LogLevel.Information, "The EFCore inbox/outbox is configured.")]
		public static partial void LogEntityFrameworkCoreInboxOutboxUsed(this ILogger logger);
	}
}
