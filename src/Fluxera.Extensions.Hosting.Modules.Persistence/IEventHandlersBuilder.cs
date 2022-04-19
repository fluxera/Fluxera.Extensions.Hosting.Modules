namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;

	public interface IEventHandlersBuilder
	{
		IEventHandlersBuilder AddEventHandlers(IEnumerable<Type> types);

		IEventHandlersBuilder AddEventHandler(Type type);

		IEventHandlersBuilder AddEventHandler<T>();
	}
}
