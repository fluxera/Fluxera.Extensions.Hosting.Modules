namespace Ordering.Domain.Messages
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Messages;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.OrderAggregate;

	[PublicAPI]
	public sealed class OrderSubmitted : IEventMessage
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
