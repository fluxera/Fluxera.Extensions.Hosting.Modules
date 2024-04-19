#undef EFCORE
#define MONGO

namespace Ordering.Infrastructure.Contexts
{
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[UsedImplicitly]
	internal sealed class OrderingRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(OrderingDbContext));
		}
	}
#elif MONGO
	[UsedImplicitly]
	internal sealed class OrderingRepositoryContext : MongoContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			options.UseDbContext<OrderingContext>();
		}
	}
#endif
}
