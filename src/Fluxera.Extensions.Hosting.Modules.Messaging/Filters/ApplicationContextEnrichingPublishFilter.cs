namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Options;

	/// <summary>
	///		A publish-filter that utilizes the <see cref="IHostEnvironment" /> to add the
	///		application context to the consume context headers.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class ApplicationContextEnrichingPublishFilter<T> : IFilter<PublishContext<T>>
		where T : class
	{
		private readonly IHostEnvironment environment;
		private readonly MessagingOptions options;

		/// <summary>
		///     Creates a new instance of the <see cref="ApplicationContextEnrichingPublishFilter{T}" /> type.
		/// </summary>
		/// <param name="environment"></param>
		/// <param name="options"></param>
		public ApplicationContextEnrichingPublishFilter(
			IHostEnvironment environment,
			IOptions<MessagingOptions> options)
		{
			this.environment = environment;
			this.options = options.Value;
		}

		/// <inheritdoc />
		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
		{
			// Executed before the message is published.
			context.Headers.Set(TransportHeaders.OriginApplicationHeaderName, this.environment.ApplicationName);

			// Here the next filter in the pipe is called.
			await next.Send(context);
		}

		/// <inheritdoc />
		public void Probe(ProbeContext context)
		{
		}
	}
}