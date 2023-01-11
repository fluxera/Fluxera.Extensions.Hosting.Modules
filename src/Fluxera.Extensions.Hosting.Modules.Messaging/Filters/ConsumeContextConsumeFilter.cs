namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MassTransit;

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

		/// <summary>
		///     Creates a new instance of the <see cref="ConsumeContextConsumeFilter{T}" /> type.
		/// </summary>
		/// <param name="consumeContextAccessor"></param>
		public ConsumeContextConsumeFilter(IConsumeContextAccessor consumeContextAccessor)
		{
			this.consumeContextAccessor = consumeContextAccessor;
		}

		/// <inheritdoc />
		public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
		{
			try
			{
				// Executed before the message is consumed.
				this.consumeContextAccessor.ConsumeContext = context;

				// Here the next filter in the pipe is called.
				await next.Send(context);
			}
			finally
			{
				// Executed after the message was consumed or an exception occurred.
				this.consumeContextAccessor.ConsumeContext = null;
			}
		}

		/// <inheritdoc />
		public void Probe(ProbeContext context)
		{
		}
	}
}
