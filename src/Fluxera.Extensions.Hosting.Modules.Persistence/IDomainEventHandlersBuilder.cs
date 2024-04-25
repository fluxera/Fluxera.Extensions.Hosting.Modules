namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a builder that configures the domain event handlers.
	/// </summary>
	[PublicAPI]
	public interface IDomainEventHandlersBuilder
	{
		/// <summary>
		///     Adds all domain event handlers available in the given assemblies.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies);

		/// <summary>
		///     Adds all domain event handlers available in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventHandlers(Assembly assembly);

		/// <summary>Adds a domain events reducer.</summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventsReducer<T>() where T : class, IDomainEventsReducer;
	}
}
