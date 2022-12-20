namespace Catalog.Domain.ProductAggregate
{
	using Catalog.Domain.Shared.ProductAggregate;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     An implementation of a generic repository that handles product instances.
	/// </summary>
	[UsedImplicitly]
	internal sealed class ProductRepository : Repository<Product, ProductId>, IProductRepository
	{
		/// <inheritdoc />
		public ProductRepository(IRepository<Product, ProductId> innerRepository)
			: base(innerRepository)
		{
		}
	}
}
