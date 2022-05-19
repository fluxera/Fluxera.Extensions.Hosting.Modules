namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A consume filter that utilizes the <see cref="IConsumeContextAccessor" /> to add
	///     the consume context to the current accessor.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class ConsumeContextConsumeFilter<T> : IFilter<ConsumeContext<T>>
		where T : class
	{
		private readonly IConsumeContextAccessor consumeContextAccessor;
		private readonly ILogger logger;

		/// <summary>
		///     Creates a new instance of the <see cref="ConsumeContextConsumeFilter{T}" /> type.
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="consumeContextAccessor"></param>
		public ConsumeContextConsumeFilter(
			ILoggerFactory loggerFactory,
			IConsumeContextAccessor consumeContextAccessor)
		{
			logger = loggerFactory.CreateLogger(GetType());
			this.consumeContextAccessor = consumeContextAccessor;
		}

		/// <inheritdoc />
		public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
		{
			try
			{
				// Executed before the message is consumed.
				consumeContextAccessor.ConsumeContext = context;

				// Here the next filter in the pipe is called.
				await next.Send(context);

				// Executed after the message was consumed.
				consumeContextAccessor.ConsumeContext = null;
			}
			catch(Exception ex)
			{
				logger.LogError($"Setting the consume context failed: {ex.Message}");

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
