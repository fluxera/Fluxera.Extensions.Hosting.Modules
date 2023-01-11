namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A public filter that utilizes the <see cref="IMessageAuthenticator" /> to
	///     validate outgoing messages.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class ValidatingPublishFilter<T> : IFilter<PublishContext<T>>
		where T : class
	{
		private readonly ILogger logger;
		private readonly IMessageValidator messageValidator;
		private readonly MessagingOptions options;

		/// <summary>
		///     Creates a new instance of the <see cref="ValidatingPublishFilter{T}" /> type.
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="messageValidator"></param>
		/// <param name="options"></param>
		public ValidatingPublishFilter(
			ILoggerFactory loggerFactory,
			IMessageValidator messageValidator,
			IOptions<MessagingOptions> options)
		{
			this.logger = loggerFactory.CreateLogger(this.GetType());
			this.messageValidator = messageValidator;
			this.options = options.Value;
		}

		/// <inheritdoc />
		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
		{
			try
			{
				if(this.options.ValidationEnabled)
				{
					// Executed before the message is send.
					this.messageValidator.ValidateMessage(context.Message);
				}

				// Here the next filter in the pipe is called.
				await next.Send(context);
			}
			catch(ValidationException ex)
			{
				this.logger.LogMessageValidationFailed(ex);

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
