namespace Ordering.Application.Contracts.Orders.Messages
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.Orders;

	[PublicAPI]
	public sealed class OrderSubmitted : IIntegrationEvent
	{
		public OrderSubmitted(OrderId orderID, decimal total)
		{
			this.OrderID = orderID;
			this.Total = total;
		}

		public OrderId OrderID { get; }

		public decimal Total { get; }
	}
}
