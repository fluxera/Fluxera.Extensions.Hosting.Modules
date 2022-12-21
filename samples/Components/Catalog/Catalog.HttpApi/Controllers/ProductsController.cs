namespace Catalog.HttpApi.Controllers
{
	using System.Collections.Generic;
	using System.Net;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Services;
	using Catalog.Domain.Shared.ProductAggregate;
	using FluentResults;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[AllowAnonymous]
	[Route("catalog/products")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductApplicationService productApplicationService;

		public ProductsController(IProductApplicationService productApplicationService)
		{
			this.productApplicationService = productApplicationService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			Result<IReadOnlyCollection<ProductDto>> result = await this.productApplicationService.GetProductsAsync();

			if(result.IsFailed)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
			}

			if(result.Value is null)
			{
				return this.NotFound();
			}

			return this.Ok(result);
		}

		[HttpGet("{id:required}")]
		public async Task<IActionResult> GetByID(ProductId id)
		{
			Result<ProductDto> result = await this.productApplicationService.GetProductAsync(id);

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
		public async Task<IActionResult> Add(ProductDto dto)
		{
			if(!this.ModelState.IsValid)
			{
				return this.BadRequest(this.ModelState);
			}

			Result<ProductDto> result = await this.productApplicationService.AddProduct(dto);

			if(result.IsFailed)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
			}

			return this.CreatedAtAction(nameof(this.GetByID), new { id = result.Value.ID }, result);
		}
	}
}
