namespace Ordering.Infrastructure.Contexts
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Ordering.Domain.Customers;
	using Ordering.Domain.Orders;

	/// <summary>
	///		Extension methods for the <see cref="ModelBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class ModelBuilderExtensions
	{
		public static void AddOrderEntity(
			this ModelBuilder modelBuilder,
			Action<EntityTypeBuilder<Order>> callback = null)
		{
			modelBuilder.ApplyConfiguration(new OrderConfiguration(callback));
		}

		public static void AddOrderItemEntity(
			this ModelBuilder modelBuilder,
			Action<EntityTypeBuilder<OrderItem>> callback = null)
		{
			modelBuilder.ApplyConfiguration(new OrderItemConfiguration(callback));
		}

		public static void AddCustomerEntity(
			this ModelBuilder modelBuilder,
			Action<EntityTypeBuilder<Customer>> callback = null)
		{
			modelBuilder.ApplyConfiguration(new CustomerConfiguration(callback));
		}
	}
}
