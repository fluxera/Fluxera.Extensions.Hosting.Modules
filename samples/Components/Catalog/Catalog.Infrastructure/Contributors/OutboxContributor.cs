namespace Catalog.Infrastructure.Contributors
{
	using Catalog.Infrastructure.Contexts;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.Contributors;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class OutboxContributor : OutboxContributor<CatalogDbContext>
	{
	}
}
