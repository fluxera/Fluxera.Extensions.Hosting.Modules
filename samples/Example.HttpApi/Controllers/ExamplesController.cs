namespace Example.HttpApi.Controllers
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Services;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[AllowAnonymous]
	[Route("examples")]
	public class ExamplesController : ControllerBase
	{
		private readonly IExampleApplicationService exampleApplicationService;

		public ExamplesController(IExampleApplicationService exampleApplicationService)
		{
			this.exampleApplicationService = exampleApplicationService;
		}

		[HttpGet("{id:required}")]
		public async Task<IActionResult> GetByID(string id)
		{
			ExampleDto result = await this.exampleApplicationService.GetExampleAsync(id);

			if(result is null)
			{
				return this.NotFound(id);
			}

			return this.Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ExampleDto dto)
		{
			ExampleDto result = await this.exampleApplicationService.AddExample(dto);

			if(result is null)
			{
				return this.NoContent();
			}

			return this.CreatedAtAction(nameof(this.GetByID), new { id = result.ID }, result);
		}
	}
}
