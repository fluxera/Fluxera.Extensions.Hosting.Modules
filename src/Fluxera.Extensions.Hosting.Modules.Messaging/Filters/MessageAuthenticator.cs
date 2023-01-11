namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Security.Authentication;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class MessageAuthenticator : IMessageAuthenticator
	{
		private readonly IPrincipalAccessor principalAccessor;

		public MessageAuthenticator(IPrincipalAccessor principalAccessor)
		{
			this.principalAccessor = principalAccessor;
		}

		/// <inheritdoc />
		public async Task AuthenticateMessageAsync<T>(SendContext<T> context) where T : class
		{
			Guard.Against.Null(context);

			// Only try to acquire an access token when no principal is available in the headers already.
			bool accessTokenExists = context.Headers.TryGetHeader(TransportHeaders.AccessTokenHeaderName, out object _);
			if(!accessTokenExists)
			{
				// Try to get an access token.
				string accessToken = await this.principalAccessor.GetAccessTokenAsync();

				if(string.IsNullOrWhiteSpace(accessToken))
				{
					throw new AuthenticationException("No access token available.");
				}

				// Set the access token on the context.
				context.Headers.Set(TransportHeaders.AccessTokenHeaderName, accessToken);
			}
		}
	}
}
