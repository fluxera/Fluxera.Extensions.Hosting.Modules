#undef EFCORE
#define MONGO

namespace Catalog.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[PublicAPI]
	internal sealed class CatalogRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(CatalogDbContext));
		}
	}
#elif MONGO
	[PublicAPI]
	public sealed class CatalogRepositoryContext : MongoContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			options.UseDbContext<CatalogMongoDbContext>();
		}
	}
#endif
}