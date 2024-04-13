namespace Ordering.HttpApi.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Ordering.Application.Contracts.Orders;

	[ApiController]
	[AllowAnonymous]
	[Route("ordering/orders")]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderApplicationService orderApplicationService;

		public OrdersController(IOrderApplicationService orderApplicationService)
		{
			this.orderApplicationService = orderApplicationService;
		}
	}
}
