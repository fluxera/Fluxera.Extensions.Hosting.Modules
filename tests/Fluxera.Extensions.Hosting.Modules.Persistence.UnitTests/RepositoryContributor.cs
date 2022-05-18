namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	public class RepositoryContributor : RepositoryContributorBase
	{
		/// <inheritdoc />
		public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder)
		{
			builder.UseFor<Customer>();
		}
	}
}
