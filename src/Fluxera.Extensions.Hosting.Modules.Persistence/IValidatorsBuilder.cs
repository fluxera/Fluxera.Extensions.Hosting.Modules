namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a builder that configures the (FluentValidation) validators.
	/// </summary>
	[PublicAPI]
	public interface IValidatorsBuilder
	{
		/// <summary>
		///     Adds all available validators in the given assemblies.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		IValidatorsBuilder AddValidatorsFromAssemblies(IEnumerable<Assembly> assemblies);

		/// <summary>
		///     Adds all available validators in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		IValidatorsBuilder AddValidatorsFromAssembly(Assembly assembly);
	}
}
