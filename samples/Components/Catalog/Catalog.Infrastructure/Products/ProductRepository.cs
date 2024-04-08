namespace Catalog.Infrastructure.Products
{
	using Catalog.Domain.Products;
	using Catalog.Domain.Shared.Products;
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
