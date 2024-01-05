namespace Ordering.Infrastructure.Contexts
{
	using System;
	using Fluxera.Repository.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Ordering.Domain.CustomerAggregate;

	public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		private readonly Action<EntityTypeBuilder<Customer>> callback;

		public CustomerConfiguration(Action<EntityTypeBuilder<Customer>> callback = null)
		{
			this.callback = callback;
		}


		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable("Customers");

			builder.OwnsOne(x => x.Name);

			builder.UseRepositoryDefaults();

			this.callback?.Invoke(builder);
		}
	}
}
