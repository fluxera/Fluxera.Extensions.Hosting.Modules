namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Principal.UnitTests
{
	using Microsoft.AspNetCore.Http;

	public class TestHttpContextAccessor : IHttpContextAccessor
	{
		/// <inheritdoc />
		public HttpContext? HttpContext { get; set; } = new DefaultHttpContext();
	}
}
