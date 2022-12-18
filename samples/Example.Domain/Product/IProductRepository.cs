namespace Catalog.Domain.Product
{
	using Catalog.Domain.Shared.Product;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a repository that handles example instances.
	/// </summary>
	[PublicAPI]
	public interface IProductRepository : IRepository<Product, ProductId>
	{
	}
}
