namespace Ordering.Infrastructure.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;
	using Ordering.Domain.CustomerAggregate;
	using Ordering.Domain.OrderAggregate;

	[UsedImplicitly]
	internal sealed class RepositoryContributor : RepositoryContributorBase
	{
		/// <inheritdoc />
		public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseFor<Order>();
			builder.UseFor<Customer>();
		}
	}
}
