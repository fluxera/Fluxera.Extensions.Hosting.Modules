namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a builder that configures the aggregates of a repository.
	/// </summary>
	[PublicAPI]
	public interface IRepositoryAggregatesBuilder
	{
		/// <summary>
		///     Uses all available aggregate types in the given assemblies.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		IRepositoryAggregatesBuilder UseFor(IEnumerable<Assembly> assemblies);

		/// <summary>
		///     Uses all available aggregate types in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		IRepositoryAggregatesBuilder UseFor(Assembly assembly);

		/// <summary>
		///     Uses all given aggregate types.
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		IRepositoryAggregatesBuilder UseFor(IEnumerable<Type> types);

		/// <summary>
		///     Uses the given aggregate type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IRepositoryAggregatesBuilder UseFor(Type type);

		/// <summary>
		///     Uses the given aggregate type.
		/// </summary>
		/// <typeparam name="TAggregateRoot"></typeparam>
		/// <returns></returns>
		IRepositoryAggregatesBuilder UseFor<TAggregateRoot>();
	}
}
