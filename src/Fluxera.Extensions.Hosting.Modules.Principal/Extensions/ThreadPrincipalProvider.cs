namespace Fluxera.Extensions.Hosting.Modules.Principal.Extensions
{
	using System.Security.Claims;
	using System.Threading;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ThreadPrincipalProvider : IPrincipalProvider
	{
		/// <inheritdoc />
		public int Position => int.MaxValue;

		/// <inheritdoc />
		public ClaimsPrincipal User => Thread.CurrentPrincipal as ClaimsPrincipal;
	}
}
