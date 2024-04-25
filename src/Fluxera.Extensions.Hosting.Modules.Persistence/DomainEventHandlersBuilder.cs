namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository;
	using Fluxera.Repository.DomainEvents;

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
		public IDomainEventHandlersBuilder AddDomainEventsReducer<T>() where T : class, IDomainEventsReducer
		{
			this.domainEventsOptionsBuilder.AddDomainEventsReducer<T>();
			return this;
		}
	}
}
