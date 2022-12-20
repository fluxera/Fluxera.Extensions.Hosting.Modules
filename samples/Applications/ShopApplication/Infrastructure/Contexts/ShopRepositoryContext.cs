namespace ShopApplication.Infrastructure.Contexts
{
	using Fluxera.Repository.EntityFrameworkCore;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ShopRepositoryContext : EntityFrameworkCoreContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
		{
			options.UseDbContext<ShopDbContext>();
		}
	}
}
