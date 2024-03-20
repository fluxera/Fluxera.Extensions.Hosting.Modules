namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A publish-filter that utilizes the <see cref="IMessageAuthenticator" /> to add
	///     an access token to the consume context headers.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public sealed class AuthenticatingPublishFilter<T> : IFilter<PublishContext<T>>
		where T : class
	{
		private readonly IMessageAuthenticator messageAuthenticator;
		private readonly MessagingOptions options;

		/// <summary>
		///     Creates a new instance of the <see cref="AuthenticatingPublishFilter{T}" /> type.
		/// </summary>
		/// <param name="messageAuthenticator"></param>
		/// <param name="options"></param>
		public AuthenticatingPublishFilter(
			IMessageAuthenticator messageAuthenticator,
			IOptions<MessagingOptions> options)
		{
			this.messageAuthenticator = messageAuthenticator;
			this.options = options.Value;
		}

		/// <inheritdoc />
		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
		{
			if(this.options.AuthenticationEnabled)
			{
				// Executed before the message is published.
				await this.messageAuthenticator.AuthenticateMessageAsync(context);
			}

			// Here the next filter in the pipe is called.
			await next.Send(context);
		}

		/// <inheritdoc />
		public void Probe(ProbeContext context)
		{
		}
	}
}
