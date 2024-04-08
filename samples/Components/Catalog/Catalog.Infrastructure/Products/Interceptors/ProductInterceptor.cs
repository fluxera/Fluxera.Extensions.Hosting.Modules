namespace Catalog.Infrastructure.Products.Interceptors
{
	using Catalog.Domain.Products;
	using Catalog.Domain.Shared.Products;
	using Fluxera.Repository.Interception;
	using JetBrains.Annotations;

	/// <summary>
	///     A repository interceptor for the example aggregate root.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductInterceptor : InterceptorBase<Product, ProductId>
	{
	}
}
