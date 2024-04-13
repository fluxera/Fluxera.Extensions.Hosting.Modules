namespace Ordering.Domain.Messages.Orders
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Messages;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.Orders;

	[PublicAPI]
	public sealed class OrderShipped : IEventMessage
	{
		public OrderShipped(OrderId orderID, decimal total)
		{
			this.OrderID = orderID;
			this.Total = total;
		}

		public OrderId OrderID { get; }

		public decimal Total { get; }
	}
}
