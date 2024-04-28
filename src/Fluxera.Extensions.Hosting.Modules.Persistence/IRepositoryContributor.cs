namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a contributor that provides settings for the repository it is registered for.
	/// </summary>
	[PublicAPI]
	public interface IRepositoryContributor
	{
		/// <summary>
		///     Configure the aggregates to use with the repository.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context);

		/// <summary>
		///     Configures the domain event handlers to use.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void ConfigureDomainEvents(IDomainEventHandlersBuilder builder, IServiceConfigurationContext context);

		/// <summary>
		///     Configures the validators to use.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context);

		/// <summary>
		///     Configures the interceptors to use.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context);

		/// <summary>
		///     Configures the caching.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void ConfigureCaching(ICachingBuilder builder, IServiceConfigurationContext context);
	}
}
