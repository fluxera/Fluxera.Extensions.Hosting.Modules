namespace Ordering.Domain.Orders
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Entity;
	using JetBrains.Annotations;
	using Ordering.Domain.Orders.DomainEvents;
	using Ordering.Domain.Shared.Orders;

	[PublicAPI]
	public sealed class Order : Entity<Order, OrderId>
	{
		private readonly IList<OrderItem> orderItems;

		private Order()
		{
			this.orderItems = new List<OrderItem>();
		}

		public Order(Address shippingAddress, Address billingAddress) : this()
		{
			this.OrderState = OrderState.Submitted;
			this.ShippingAddress = shippingAddress;
			this.BillingAddress = billingAddress;

			this.RaiseDomainEvent(new OrderSubmitted(this));
		}

		public OrderState OrderState { get; private set; }

		public Address ShippingAddress { get; private set; }

		public Address BillingAddress { get; private set; }

		public IReadOnlyCollection<OrderItem> OrderItems => this.orderItems.AsReadOnly();

		public decimal Total => this.orderItems.Sum(x => x.Total);

		public void AddOrderItem(string productId, decimal unitPrice, int amount = 1)
		{
			OrderItem existingOrderItemForProduct = this.orderItems.SingleOrDefault(o => o.ProductId == productId);

			if(existingOrderItemForProduct is not null)
			{
				existingOrderItemForProduct.IncrementAmount(amount);
			}
			else
			{
				OrderItem orderItem = new OrderItem(productId, unitPrice, amount);
				this.orderItems.Add(orderItem);
			}
		}

		public void SetShippedState()
		{
			if(this.OrderState != OrderState.Submitted)
			{
				throw new InvalidOperationException("The order state must be 'Submitted'.");
			}

			this.OrderState = OrderState.Shipped;
			this.RaiseDomainEvent(new OrderSubmitted(this));
		}
	}
}
