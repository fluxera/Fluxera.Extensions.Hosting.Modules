namespace Catalog.Application.Contracts.Products
{
	using Catalog.Domain.Shared.Products;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Entities;
	using JetBrains.Annotations;

	/// <summary>
	///     A dto that provides the data of an example.
	/// </summary>
	[PublicAPI]
	public sealed class ProductDto : EntityDto<ProductId>
	{
		/// <summary>
		///     Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Gets or sets the price.
		/// </summary>
		public decimal Price { get; set; }
	}
}
