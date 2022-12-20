namespace Catalog.Infrastructure.Contexts
{
	using System;
	using Catalog.Domain.ProductAggregate;
	using Fluxera.Repository.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		private readonly Action<EntityTypeBuilder<Product>> callback;

		public ProductConfiguration(Action<EntityTypeBuilder<Product>> callback = null)
		{
			this.callback = callback;
		}

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");

			builder.Property(x => x.Price).HasColumnType("money");

			builder.UseRepositoryDefaults();

			this.callback?.Invoke(builder);
		}
	}
}
