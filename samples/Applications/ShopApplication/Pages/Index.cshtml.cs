namespace ShopApplication.Pages
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products;
	using FluentResults;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> logger;
		private readonly IProductApplicationService productApplicationService;

		public IndexModel(ILogger<IndexModel> logger, IProductApplicationService productApplicationService)
		{
			this.logger = logger;
			this.productApplicationService = productApplicationService;
		}

		public IReadOnlyCollection<ProductDto> Products { get; set; }

		public async Task OnGetAsync()
		{
			Result<IReadOnlyCollection<ProductDto>> result = await this.productApplicationService.GetProductsAsync();
			this.Products = result.ValueOrDefault;
		}
	}
}
