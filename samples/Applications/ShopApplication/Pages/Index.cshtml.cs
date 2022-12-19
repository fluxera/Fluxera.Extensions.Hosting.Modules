namespace ShopApplication.Pages
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Services;
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

		public async Task OnGetAsync()
		{
		}
	}
}
