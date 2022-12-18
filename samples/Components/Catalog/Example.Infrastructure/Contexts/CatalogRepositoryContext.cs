namespace Catalog.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class CatalogRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext<CatalogDbContext>();
		}
	}
}
