namespace Catalog.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;

	internal sealed class CatalogRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext(typeof(CatalogDbContext));
		}
	}
}
