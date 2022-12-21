namespace Catalog.Domain.Shared.ProductAggregate
{
	using Fluxera.StronglyTypedId;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ProductId : StronglyTypedId<ProductId, string>
	{
		/// <inheritdoc />
		public ProductId(string value) : base(value)
		{
		}
	}
}
