namespace Ordering.Infrastructure.Contexts
{
	using System;
	using Fluxera.Repository.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Ordering.Domain.OrderAggregate;

	public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		private readonly Action<EntityTypeBuilder<Order>> callback;

		public OrderConfiguration(Action<EntityTypeBuilder<Order>> callback = null)
		{
			this.callback = callback;
		}

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");

			builder.OwnsOne(x => x.ShippingAddress);

			builder.OwnsOne(x => x.BillingAddress);

			builder.HasMany(x => x.OrderItems);

			builder.UseRepositoryDefaults();

			this.callback?.Invoke(builder);
		}
	}
}
