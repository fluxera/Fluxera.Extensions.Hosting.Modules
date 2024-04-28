namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a builder that configures the domain event handlers.
	/// </summary>
	[PublicAPI]
	public interface IDomainEventHandlersBuilder
	{
		/// <summary>Adds a domain events reducer.</summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventsReducer<T>() where T : class, IDomainEventsReducer;
	}
}
