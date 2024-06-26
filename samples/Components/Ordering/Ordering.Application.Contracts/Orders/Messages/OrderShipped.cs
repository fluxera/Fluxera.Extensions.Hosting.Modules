﻿namespace Ordering.Application.Contracts.Orders.Messages
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.Orders;

	[PublicAPI]
	public sealed class OrderShipped : IIntegrationEvent
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
