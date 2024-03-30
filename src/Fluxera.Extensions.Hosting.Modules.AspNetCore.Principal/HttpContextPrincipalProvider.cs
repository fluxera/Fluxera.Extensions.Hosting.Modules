namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Principal
{
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;
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
		public ClaimsPrincipal User => httpContextAccessor.HttpContext?.User;

		/// <inheritdoc />
		public async Task<string> GetAccessTokenAsync()
		{
			HttpContext httpContext = httpContextAccessor.HttpContext;
			if(httpContext != null)
			{
				return await httpContext.GetTokenAsync("access_token");
			}

			return null;
		}
	}
}
