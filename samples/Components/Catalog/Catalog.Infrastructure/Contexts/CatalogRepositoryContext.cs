#undef EFCORE
#define MONGO

namespace Catalog.Infrastructure.Contexts
{
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

#if EFCORE
	[UsedImplicitly]
	internal sealed class CatalogRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(CatalogDbContext));
		}
	}
#elif MONGO
	[UsedImplicitly]
	internal sealed class CatalogRepositoryContext : MongoContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			options.UseDbContext<CatalogContext>();
		}
	}
#endif
}