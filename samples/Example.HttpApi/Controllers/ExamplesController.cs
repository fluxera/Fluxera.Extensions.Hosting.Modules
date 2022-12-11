namespace Example.HttpApi.Controllers
{
	using System.Net;
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Services;
	using Example.Domain.Shared.Example;
	using FluentResults;
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
		public async Task<IActionResult> GetByID(ExampleId id)
		{
			Result<ExampleDto> result = await this.exampleApplicationService.GetExampleAsync(id);

			if(result.IsFailed)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
			}

			if(result.Value is null)
			{
				return this.NotFound(id);
			}

			return this.Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ExampleDto dto)
		{
			if(!this.ModelState.IsValid)
			{
				return this.BadRequest(this.ModelState);
			}

			Result<ExampleDto> result = await this.exampleApplicationService.AddExample(dto);

			if(result.IsFailed)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
			}

			return this.CreatedAtAction(nameof(this.GetByID), new { id = result.Value.ID }, result);
		}
	}
}
