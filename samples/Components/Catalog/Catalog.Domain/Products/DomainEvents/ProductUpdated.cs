namespace Catalog.Domain.Products.DomainEvents
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductUpdated : IDomainEvent
	{
		public ProductUpdated(ProductId id)
		{
			this.ID = id;
		}

		public ProductId ID { get; }
	}
}
