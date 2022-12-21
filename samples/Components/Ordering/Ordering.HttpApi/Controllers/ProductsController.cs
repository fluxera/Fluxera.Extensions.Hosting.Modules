namespace Ordering.HttpApi.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Ordering.Application.Contracts.Services;

	[ApiController]
	[AllowAnonymous]
	[Route("ordering/orders")]
	public class ProductsController : ControllerBase
	{
		private readonly IOrderApplicationService productApplicationService;

		public ProductsController(IOrderApplicationService productApplicationService)
		{
			this.productApplicationService = productApplicationService;
		}
	}
}
