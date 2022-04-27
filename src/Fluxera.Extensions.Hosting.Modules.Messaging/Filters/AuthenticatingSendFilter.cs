namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	[PublicAPI]
	public sealed class AuthenticatingSendFilter<T> : IFilter<SendContext<T>>
		where T : class
	{
		private readonly ILogger logger;
		private readonly IMessageAuthenticator messageAuthenticator;

		public AuthenticatingSendFilter(
			ILoggerFactory loggerFactory,
			IMessageAuthenticator messageAuthenticator)
		{
			this.logger = loggerFactory.CreateLogger(this.GetType());
			this.messageAuthenticator = messageAuthenticator;
		}

		/// <inheritdoc />
		public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
		{
			try
			{
				// Executed before the message is send.
				await this.messageAuthenticator.AuthenticateMessage(context);

				// Here the next filter in the pipe is called.
				await next.Send(context);

				// Executed after the message was sent.
				// ...
			}
			catch(Exception ex)
			{
				this.logger.LogError($"The message authentication failed: {ex.Message}");

				// Propagate the exception up the call stack.
				throw;
			}
		}

		/// <inheritdoc />
		public void Probe(ProbeContext context)
		{
		}
	}
}
