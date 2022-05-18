namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Entity.DomainEvents;
	using Fluxera.Repository;

	internal sealed class DomainEventHandlersBuilder : IDomainEventHandlersBuilder
	{
		private readonly IDomainEventsOptionsBuilder domainEventsOptionsBuilder;

		public DomainEventHandlersBuilder(IDomainEventsOptionsBuilder domainEventsOptionsBuilder)
		{
			this.domainEventsOptionsBuilder = domainEventsOptionsBuilder;
		}

		/// <inheritdoc />
		public IDomainEventHandlersBuilder AddDomainEventHandlers(IEnumerable<Assembly> assemblies)
		{
			this.domainEventsOptionsBuilder.AddDomainEventHandlers(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IDomainEventHandlersBuilder AddDomainEventHandlers(Assembly assembly)
		{
			this.domainEventsOptionsBuilder.AddDomainEventHandlers(assembly);
			return this;
		}

		/// <inheritdoc />
		public IDomainEventHandlersBuilder AddDomainEventHandlers(IEnumerable<Type> types)
		{
			this.domainEventsOptionsBuilder.AddDomainEventHandlers(types);
			return this;
		}

		/// <inheritdoc />
		public IDomainEventHandlersBuilder AddDomainEventHandler(Type type)
		{
			this.domainEventsOptionsBuilder.AddDomainEventHandler(type);
			return this;
		}

		/// <inheritdoc />
		public IDomainEventHandlersBuilder AddDomainEventHandler<T>() where T : IDomainEventHandler
		{
			this.domainEventsOptionsBuilder.AddDomainEventHandler<T>();
			return this;
		}
	}
}
