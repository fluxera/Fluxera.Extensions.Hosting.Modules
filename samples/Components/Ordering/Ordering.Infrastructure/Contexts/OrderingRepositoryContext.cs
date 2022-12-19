namespace Ordering.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class OrderingRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext<OrderingDbContext>();
		}
	}
}
