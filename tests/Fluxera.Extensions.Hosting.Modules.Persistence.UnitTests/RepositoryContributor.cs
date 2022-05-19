namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	public class RepositoryContributor : RepositoryContributorBase
	{
		/// <inheritdoc />
		public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseFor<Customer>();
		}
	}
}
