namespace Catalog.Domain.Product.Events
{
	using Catalog.Domain.Shared.Product;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductWasUpgraded : IDomainEvent
	{
		public ProductWasUpgraded(ProductId productId, string productName)
		{
			this.ProductId = productId;
			this.ProductName = productName;
		}

		public ProductId ProductId { get; }

		public string ProductName { get; }
	}
}
