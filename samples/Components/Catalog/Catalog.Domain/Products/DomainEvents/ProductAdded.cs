namespace Catalog.Domain.Products.DomainEvents
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.DomainEvents.Abstractions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductAdded : IDomainEvent
	{
		public ProductAdded(ProductId id)
		{
			this.ID = id;
		}

		public ProductId ID { get; }
	}
}
