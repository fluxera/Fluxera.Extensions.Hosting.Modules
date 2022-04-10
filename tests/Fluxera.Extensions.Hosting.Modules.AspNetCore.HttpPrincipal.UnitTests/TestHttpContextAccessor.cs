namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpPrincipal.UnitTests
{
	using Microsoft.AspNetCore.Http;

	public class TestHttpContextAccessor : IHttpContextAccessor
	{
		/// <inheritdoc />
		public HttpContext? HttpContext { get; set; } = new DefaultHttpContext();
	}
}
