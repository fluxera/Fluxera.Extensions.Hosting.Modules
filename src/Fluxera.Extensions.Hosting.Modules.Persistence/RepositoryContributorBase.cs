namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using JetBrains.Annotations;

	/// <summary>
	///     A base implementation to support overriding the methods one actually uses.
	/// </summary>
	[PublicAPI]
	public abstract class RepositoryContributorBase : IRepositoryContributor
	{
		/// <inheritdoc />
		public virtual void ConfigureAggregates(IRepositoryAggregatesBuilder builder)
		{
		}

		/// <inheritdoc />
		public virtual void ConfigureDomainEventHandlers(IDomainEventHandlersBuilder builder)
		{
		}

		/// <inheritdoc />
		public virtual void ConfigureValidators(IValidatorsBuilder builder)
		{
		}

		/// <inheritdoc />
		public virtual void ConfigureInterceptors(IInterceptorsBuilder builder)
		{
		}

		/// <inheritdoc />
		public virtual void ConfigureCaching(ICachingBuilder builder)
		{
		}
	}
}
