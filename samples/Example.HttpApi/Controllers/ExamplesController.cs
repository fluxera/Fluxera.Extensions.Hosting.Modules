namespace Example.HttpApi.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("examples")]
	public class ExamplesController : ControllerBase
	{
		[HttpGet("status/{statusCode}")]
		public IActionResult Index(int statusCode)
		{
			return this.StatusCode(statusCode);
		}
	}
}
