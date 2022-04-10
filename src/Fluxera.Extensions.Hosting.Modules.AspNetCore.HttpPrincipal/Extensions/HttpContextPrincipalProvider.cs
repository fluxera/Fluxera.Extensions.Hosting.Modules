namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpPrincipal.Extensions
{
	using System.Security.Claims;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;

	[UsedImplicitly]
	internal sealed class HttpContextPrincipalProvider : IPrincipalProvider
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public HttpContextPrincipalProvider(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		/// <inheritdoc />
		public int Position => 0;

		/// <inheritdoc />
		public ClaimsPrincipal User => this.httpContextAccessor.HttpContext?.User;
	}
}
