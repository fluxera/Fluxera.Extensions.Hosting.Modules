namespace Ordering.Infrastructure.Contexts
{
	using System;
	using Fluxera.Repository.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Ordering.Domain.Orders;

	public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		private readonly Action<EntityTypeBuilder<OrderItem>> callback;

		public OrderItemConfiguration(Action<EntityTypeBuilder<OrderItem>> callback = null)
		{
			this.callback = callback;
		}


		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems");

			builder.Property(x => x.UnitPrice).HasColumnType("money");

			builder.UseRepositoryDefaults();

			this.callback?.Invoke(builder);
		}
	}
}
