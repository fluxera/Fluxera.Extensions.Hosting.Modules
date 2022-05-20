namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Principal.UnitTests
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;

	[UsedImplicitly]
	public class TestHttpContextAccessor : IHttpContextAccessor
	{
		/// <inheritdoc />
		public HttpContext HttpContext { get; set; } = new DefaultHttpContext();
	}
}
