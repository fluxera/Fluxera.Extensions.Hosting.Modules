namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	[PublicAPI]
	public sealed class ValidatingPublishFilter<T> : IFilter<PublishContext<T>>
		where T : class
	{
		private readonly ILogger logger;
		private readonly IMessageValidator messageValidator;

		public ValidatingPublishFilter(
			ILoggerFactory loggerFactory,
			IMessageValidator messageValidator)
		{
			this.logger = loggerFactory.CreateLogger(this.GetType());
			this.messageValidator = messageValidator;
		}

		/// <inheritdoc />
		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
		{
			try
			{
				// Executed before the message is published.
				this.messageValidator.ValidateMessage(context.Message);

				// Here the next filter in the pipe is called.
				await next.Send(context);

				// Executed after the message was published.
				// ...
			}
			catch(ValidationException ex)
			{
				this.logger.LogError($"The message was not valid: {ex.Message}");

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
