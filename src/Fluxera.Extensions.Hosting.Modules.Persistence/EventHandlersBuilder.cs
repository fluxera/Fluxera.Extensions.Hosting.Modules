namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Repository;

	internal sealed class EventHandlersBuilder : IEventHandlersBuilder
	{
		private readonly IDomainEventsOptionsBuilder domainHandlerOptionsBuilder;

		public EventHandlersBuilder(IDomainEventsOptionsBuilder domainHandlerOptionsBuilder)
		{
			this.domainHandlerOptionsBuilder = domainHandlerOptionsBuilder;
		}

		/// <inheritdoc />
		public IEventHandlersBuilder AddEventHandlers(IEnumerable<Type> types)
		{
			this.domainHandlerOptionsBuilder.AddEventHandlers(types);
			return this;
		}

		/// <inheritdoc />
		public IEventHandlersBuilder AddEventHandler(Type type)
		{
			this.domainHandlerOptionsBuilder.AddEventHandler(type);
			return this;
		}

		/// <inheritdoc />
		public IEventHandlersBuilder AddEventHandler<T>()
		{
			this.domainHandlerOptionsBuilder.AddEventHandler<T>();
			return this;
		}
	}
}
