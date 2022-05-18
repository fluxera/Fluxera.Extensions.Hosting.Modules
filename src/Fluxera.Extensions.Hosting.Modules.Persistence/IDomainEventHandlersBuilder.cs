namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Entity.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IDomainEventHandlersBuilder
	{
		IDomainEventHandlersBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies);

		IDomainEventHandlersBuilder AddDomainEventHandlers(Assembly assembly);

		IDomainEventHandlersBuilder AddDomainEventHandlers(IEnumerable<Type> types);

		IDomainEventHandlersBuilder AddDomainEventHandler(Type type);

		IDomainEventHandlersBuilder AddDomainEventHandler<T>() where T : IDomainEventHandler;
	}
}
