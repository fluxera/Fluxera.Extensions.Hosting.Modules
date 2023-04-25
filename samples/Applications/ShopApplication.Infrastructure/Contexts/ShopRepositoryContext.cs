#define EFCORE
#undef MONGO

namespace ShopApplication.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[PublicAPI]
	public sealed class ShopRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext<ShopDbContext>();
		}
	}
#elif MONGO
	[PublicAPI]
	public sealed class ShopRepositoryContext : MongoContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			options.UseDbContext<ShopMongoDbContext>();
		}
	}
#endif
}
