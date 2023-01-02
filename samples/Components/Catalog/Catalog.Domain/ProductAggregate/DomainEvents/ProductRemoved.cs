namespace Catalog.Domain.ProductAggregate.DomainEvents
{
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductRemoved : ItemRemoved<Product, ProductId>
	{
		/// <inheritdoc />
		public ProductRemoved(ProductId id, Product item)
			: base(id, item)
		{
		}
	}
}
