namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Security.Claims;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ThreadPrincipalProvider : IPrincipalProvider
	{
		/// <inheritdoc />
		public int Position => int.MaxValue;

		/// <inheritdoc />
		public ClaimsPrincipal User => Thread.CurrentPrincipal as ClaimsPrincipal;

		/// <inheritdoc />
		public Task<string> GetAccessTokenAsync()
		{
			return Task.FromResult(null as string);
		}
	}
}
