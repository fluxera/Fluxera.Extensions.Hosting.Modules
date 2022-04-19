namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a contributor that provides settings for the repository it is registered for..
	/// </summary>
	[PublicAPI]
	public interface IRepositoryContributor
	{
		/// <summary>
		///     Configure the aggregates to use with the repository.
		/// </summary>
		/// <param name="builder"></param>
		void ConfigureAggregates(IRepositoryAggregatesBuilder builder);

		/// <summary>
		///     Configures the domain event handlers to use.
		/// </summary>
		/// <param name="builder"></param>
		void ConfigureEventHandling(IEventHandlersBuilder builder);

		/// <summary>
		///     Configures the validators to use.
		/// </summary>
		/// <param name="builder"></param>
		void ConfigureValidation(IValidatorBuilder builder);

		/// <summary>
		///     Configures the interceptors to use.
		/// </summary>
		/// <param name="builder"></param>
		void ConfigureInterception(IInterceptionBuilder builder);

		/// <summary>
		///     Configures the caching.
		/// </summary>
		/// <param name="builder"></param>
		void ConfigureCaching(ICachingBuilder builder);
	}
}
