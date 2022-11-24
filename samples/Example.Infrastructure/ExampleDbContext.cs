namespace Example.Infrastructure
{
	using MassTransit;
	using Microsoft.EntityFrameworkCore;

	public sealed class ExampleDbContext : DbContext
	{
		public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
			: base(options)
		{
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddInboxStateEntity();
			modelBuilder.AddOutboxMessageEntity();
			modelBuilder.AddOutboxStateEntity();
		}
	}
}
