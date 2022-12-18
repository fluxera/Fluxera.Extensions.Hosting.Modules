namespace Catalog.Infrastructure.Product.Interceptors
{
	using Catalog.Domain.Product;
	using Catalog.Domain.Shared.Product;
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
