namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;

	public class RepositoryContributor : RepositoryContributorBase
	{
		/// <inheritdoc />
		public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseFor<Customer>();
		}
	}
}
