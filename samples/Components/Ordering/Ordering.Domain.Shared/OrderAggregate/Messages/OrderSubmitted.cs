﻿namespace Ordering.Domain.Shared.OrderAggregate.Messages
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages;
	using JetBrains.Annotations;

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
