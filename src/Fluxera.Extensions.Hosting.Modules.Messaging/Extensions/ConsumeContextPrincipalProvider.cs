namespace Fluxera.Extensions.Hosting.Modules.Messaging.Extensions
{
	using System.Security.Claims;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ConsumeContextPrincipalProvider : IPrincipalProvider
	{
		private readonly IConsumeContextAccessor consumeContextAccessor;

		public ConsumeContextPrincipalProvider(IConsumeContextAccessor consumeContextAccessor)
		{
			this.consumeContextAccessor = consumeContextAccessor;
		}

		/// <inheritdoc />
		public int Position => int.MaxValue;

		/// <inheritdoc />
		public ClaimsPrincipal User
		{
			get
			{
				ClaimsPrincipal principal = null;

				//string accessToken = this.consumeContextAccessor.ConsumeContext.GetAccessToken();
				//if(!string.IsNullOrWhiteSpace(accessToken))
				//{
				//	principal = this.principalFactory.CreatePrincipal(accessToken);
				//}

				return principal;
			}
		}
	}
}
