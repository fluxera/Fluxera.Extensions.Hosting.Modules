namespace Ordering.Domain.OrderAggregate
{
	using Fluxera.Entity;
	using JetBrains.Annotations;
	using Ordering.Domain.Shared.OrderAggregate;

	[PublicAPI]
	public sealed class OrderItem : Entity<OrderItem, OrderItemId>
	{
		private OrderItem()
		{
		}

		public OrderItem(string productId, decimal unitPrice, int amount = 1)
		{
			this.ProductId = productId;
			this.UnitPrice = unitPrice;
			this.Amount = amount;
		}

		public string ProductId { get; private set; }

		public int Amount { get; private set; }

		public decimal UnitPrice { get; private set; }

		public decimal Total => this.Amount * this.UnitPrice;

		public void IncrementAmount(int amount)
		{
			this.Amount += amount;
		}
	}
}
