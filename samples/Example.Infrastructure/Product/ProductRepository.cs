namespace Catalog.Infrastructure.Product
{
	using Catalog.Domain.Product;
	using Catalog.Domain.Shared.Product;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     An implementation of a generic repository that handles example instances.
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
