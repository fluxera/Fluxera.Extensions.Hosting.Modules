namespace Catalog.HttpApi.Controllers
{
	using System.Net;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
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
			ResultDto<ProductDto[]> result = await this.productApplicationService.GetProductsAsync();

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
			ResultDto<ProductDto> result = await this.productApplicationService.GetProductAsync(id);

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

			ResultDto<ProductDto> result = await this.productApplicationService.AddProduct(dto);

			if(result.IsFailed)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
			}

			return this.CreatedAtAction(nameof(this.GetByID), new { id = result.Value.ID }, result);
		}
	}
}
