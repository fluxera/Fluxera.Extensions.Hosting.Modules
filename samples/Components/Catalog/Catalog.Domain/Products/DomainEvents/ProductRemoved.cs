namespace Catalog.Domain.Products.DomainEvents
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductRemoved : IDomainEvent
	{
		public ProductRemoved(ProductId id)
		{
			this.ID = id;
		}

		public ProductId ID { get; }
	}
}
