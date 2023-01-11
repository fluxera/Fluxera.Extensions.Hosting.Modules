namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Security.Authentication;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A send filter that utilizes the <see cref="IMessageAuthenticator" /> to add
	///     an access token to the consume context headers.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class AuthenticatingSendFilter<T> : IFilter<SendContext<T>>
		where T : class
	{
		private readonly ILogger logger;
		private readonly IMessageAuthenticator messageAuthenticator;

		/// <summary>
		///     Creates a new instance of the <see cref="AuthenticatingSendFilter{T}" /> type.
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="messageAuthenticator"></param>
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
				await this.messageAuthenticator.AuthenticateMessageAsync(context);

				// Here the next filter in the pipe is called.
				await next.Send(context);
			}
			catch(AuthenticationException ex)
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
