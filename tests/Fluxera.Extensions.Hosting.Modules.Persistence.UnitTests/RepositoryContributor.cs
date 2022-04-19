namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	public class RepositoryContributor : IRepositoryContributor
	{
		/// <inheritdoc />
		public void ConfigureAggregates(IRepositoryAggregatesBuilder builder)
		{
			builder.UseFor<Customer>();
		}

		/// <inheritdoc />
		public void ConfigureEventHandling(IEventHandlersBuilder builder)
		{
		}

		/// <inheritdoc />
		public void ConfigureValidation(IValidatorBuilder builder)
		{
		}

		/// <inheritdoc />
		public void ConfigureInterception(IInterceptionBuilder builder)
		{
		}

		/// <inheritdoc />
		public void ConfigureCaching(ICachingBuilder builder)
		{
		}
	}
}
