namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
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
		IValidatorsBuilder AddValidators(IEnumerable<Assembly> assemblies);

		/// <summary>
		///     Adds all available validators in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		IValidatorsBuilder AddValidators(Assembly assembly);

		/// <summary>
		///     Adds all given validator types.
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		IValidatorsBuilder AddValidators(IEnumerable<Type> types);

		/// <summary>
		///     Adds the given validator type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IValidatorsBuilder AddValidator(Type type);

		/// <summary>
		///     Adds the given validator type.
		/// </summary>
		/// <typeparam name="TValidator"></typeparam>
		/// <returns></returns>
		IValidatorsBuilder AddValidator<TValidator>() where TValidator : FluentValidation.IValidator;
	}
}
