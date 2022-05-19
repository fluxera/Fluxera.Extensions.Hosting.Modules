namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository.Interception;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a builder that configures the interceptors.
	/// </summary>
	[PublicAPI]
	public interface IInterceptorsBuilder
	{
		/// <summary>
		///     Adds all available interceptors in the given assemblies.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		IInterceptorsBuilder AddInterceptors(IEnumerable<Assembly> assemblies);

		/// <summary>
		///     Adds all available interceptors in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		IInterceptorsBuilder AddInterceptors(Assembly assembly);

		/// <summary>
		///     Adds the given interceptor types.
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		IInterceptorsBuilder AddInterceptors(IEnumerable<Type> types);

		/// <summary>
		///     Adds the given interceptor type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IInterceptorsBuilder AddInterceptor(Type type);

		/// <summary>
		///     Adds the given interceptor type.
		/// </summary>
		/// <typeparam name="TInterceptor"></typeparam>
		/// <returns></returns>
		IInterceptorsBuilder AddInterceptor<TInterceptor>() where TInterceptor : IInterceptor;
	}
}
