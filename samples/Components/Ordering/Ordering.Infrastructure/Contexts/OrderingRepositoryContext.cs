#undef EFCORE
#define MONGO

namespace Ordering.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[PublicAPI]
	internal sealed class OrderingRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(OrderingDbContext));
		}
	}
#elif MONGO
	[PublicAPI]
	public sealed class OrderingRepositoryContext : MongoContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			options.UseDbContext<OrderingMongoDbContext>();
		}
	}
#endif
}
