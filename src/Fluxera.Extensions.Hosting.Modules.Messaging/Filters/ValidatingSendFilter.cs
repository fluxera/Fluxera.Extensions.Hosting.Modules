namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A send filter that utilizes the <see cref="IMessageValidator" /> to
	///     validate outgoing messages.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class ValidatingSendFilter<T> : IFilter<SendContext<T>>
		where T : class
	{
		private readonly ILogger logger;
		private readonly IMessageValidator messageValidator;

		/// <summary>
		///     Creates a new instance of the <see cref="ValidatingSendFilter{T}" /> type.
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="messageValidator"></param>
		public ValidatingSendFilter(
			ILoggerFactory loggerFactory,
			IMessageValidator messageValidator)
		{
			logger = loggerFactory.CreateLogger(GetType());
			this.messageValidator = messageValidator;
		}

		/// <inheritdoc />
		public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
		{
			try
			{
				// Executed before the message is send.
				messageValidator.ValidateMessage(context.Message);

				// Here the next filter in the pipe is called.
				await next.Send(context);

				// Executed after the message was sent.
				// ...
			}
			catch(ValidationException ex)
			{
				logger.LogError($"The message was not valid: {ex.Message}");

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
