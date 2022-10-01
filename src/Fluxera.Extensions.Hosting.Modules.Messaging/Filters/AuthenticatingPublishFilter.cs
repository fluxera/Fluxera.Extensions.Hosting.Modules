namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A publish filter that utilizes the <see cref="IMessageAuthenticator" /> to add
	///     an access token to the consume context headers.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class AuthenticatingPublishFilter<T> : IFilter<PublishContext<T>>
		where T : class
	{
		private readonly ILogger logger;
		private readonly IMessageAuthenticator messageAuthenticator;

		/// <summary>
		///     Creates a new instance of the <see cref="AuthenticatingPublishFilter{T}" /> type.
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="messageAuthenticator"></param>
		public AuthenticatingPublishFilter(
			ILoggerFactory loggerFactory,
			IMessageAuthenticator messageAuthenticator)
		{
			this.logger = loggerFactory.CreateLogger(this.GetType());
			this.messageAuthenticator = messageAuthenticator;
		}

		/// <inheritdoc />
		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
		{
			try
			{
				// Executed before the message is published.
				await this.messageAuthenticator.AuthenticateMessage(context);

				// Here the next filter in the pipe is called.
				await next.Send(context);

				// Executed after the message was published.
				// ...
			}
			catch(Exception ex)
			{
				this.logger.LogMessageAuthenticationFailed(ex);

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
