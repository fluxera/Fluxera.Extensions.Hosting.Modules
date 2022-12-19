namespace Ordering.Infrastructure.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.Contributors;
	using JetBrains.Annotations;
	using Ordering.Infrastructure.Contexts;

	[UsedImplicitly]
	internal sealed class OutboxContributor : OutboxContributor<OrderingDbContext>
	{
	}
}
