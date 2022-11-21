namespace Example.HttpApi.Controllers
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Services;
	using Example.Domain.Shared.Example;
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[AllowAnonymous]
	[Route("examples")]
	public class ExamplesController : ControllerBase
	{
		// https://github.com/jbogard/MediatR/wiki
		private readonly ISender sender;

		public ExamplesController(ISender sender)
		{
			this.sender = sender;
		}

		[HttpGet("{id:required}")]
		public async Task<IActionResult> GetByID(ExampleId id)
		{
			ExampleDto result = await this.sender.Send(new GetExampleRequest(id));

			if(result is null)
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

			ExampleDto result = await this.sender.Send(new AddExampleRequest(dto));

			return this.CreatedAtAction(nameof(this.GetByID), new { id = result.ID }, result);
		}
	}
}
