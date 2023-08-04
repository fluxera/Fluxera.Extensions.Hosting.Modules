namespace Ordering.HttpApi.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.OData.Routing.Controllers;
	using Ordering.Application.Contracts.Orders;

	[ApiController]
	[AllowAnonymous]
	[Route("ordering/orders")]
	public class OrdersController : ODataController
	{
		private readonly IOrderApplicationService orderApplicationService;

		public OrdersController(IOrderApplicationService orderApplicationService)
		{
			this.orderApplicationService = orderApplicationService;
		}
	}
}
