namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using System.Reflection;
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
	}
}
