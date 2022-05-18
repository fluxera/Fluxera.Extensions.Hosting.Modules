namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IRepositoryAggregatesBuilder
	{
		IRepositoryAggregatesBuilder UseFor(IEnumerable<Assembly> assemblies);

		IRepositoryAggregatesBuilder UseFor(Assembly assembly);

		IRepositoryAggregatesBuilder UseFor(IEnumerable<Type> types);

		IRepositoryAggregatesBuilder UseFor(Type type);

		IRepositoryAggregatesBuilder UseFor<TAggregateRoot>();
	}
}
