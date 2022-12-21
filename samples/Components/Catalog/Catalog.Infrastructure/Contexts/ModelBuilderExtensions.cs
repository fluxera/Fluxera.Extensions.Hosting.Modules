namespace Catalog.Infrastructure.Contexts
{
	using System;
	using Catalog.Domain.ProductAggregate;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///		Extension methods for the <see cref="ModelBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class ModelBuilderExtensions
	{
		public static void AddProductEntity(
			this ModelBuilder modelBuilder,
			Action<EntityTypeBuilder<Product>> callback = null)
		{
			modelBuilder.ApplyConfiguration(new ProductConfiguration(callback));
		}
	}
}
