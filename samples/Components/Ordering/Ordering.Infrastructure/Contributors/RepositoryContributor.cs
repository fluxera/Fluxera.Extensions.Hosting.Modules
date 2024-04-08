namespace Ordering.Infrastructure.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;
	using Ordering.Domain.Customers;
	using Ordering.Domain.Orders;

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
