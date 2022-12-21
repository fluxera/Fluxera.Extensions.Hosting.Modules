namespace Catalog.Infrastructure.ProductAggregate.Interceptors
{
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.Shared.ProductAggregate;
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
