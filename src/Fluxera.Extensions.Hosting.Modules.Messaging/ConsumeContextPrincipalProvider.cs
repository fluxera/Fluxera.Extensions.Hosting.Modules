namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ConsumeContextPrincipalProvider : IPrincipalProvider
	{
		private readonly IConsumeContextAccessor consumeContextAccessor;
		private readonly IPrincipalFactory principalFactory;

		public ConsumeContextPrincipalProvider(
			IConsumeContextAccessor consumeContextAccessor,
			IPrincipalFactory principalFactory)
		{
			this.consumeContextAccessor = consumeContextAccessor;
			this.principalFactory = principalFactory;
		}

		/// <inheritdoc />
		public int Position => int.MaxValue;

		/// <inheritdoc />
		public ClaimsPrincipal User
		{
			get
			{
				ClaimsPrincipal principal = null;

				string accessToken = consumeContextAccessor.ConsumeContext.GetAccessToken();
				if(!string.IsNullOrWhiteSpace(accessToken))
				{
					principal = principalFactory.CreatePrincipal(accessToken);
				}

				return principal;
			}
		}

		/// <inheritdoc />
		public Task<string> GetAccessTokenAsync()
		{
			string accessToken = consumeContextAccessor.ConsumeContext.GetAccessToken();
			return Task.FromResult(accessToken);
		}
	}
}
