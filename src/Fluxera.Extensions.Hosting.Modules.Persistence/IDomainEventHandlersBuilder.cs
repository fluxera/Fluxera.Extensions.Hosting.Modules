namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Entity.DomainEvents;
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

		/// <summary>
		///     Adds all given domain event handler types.
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventHandlers(IEnumerable<Type> types);

		/// <summary>
		///     Adds the given domain event handler type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventHandler(Type type);

		/// <summary>
		///     Adds the given domain event handler type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IDomainEventHandlersBuilder AddDomainEventHandler<T>() where T : IDomainEventHandler;
	}
}
