namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IValidatorsBuilder
	{
		IValidatorsBuilder AddValidators(IEnumerable<Assembly> assemblies);

		IValidatorsBuilder AddValidators(Assembly assembly);

		IValidatorsBuilder AddValidators(IEnumerable<Type> types);

		IValidatorsBuilder AddValidator(Type type);

		IValidatorsBuilder AddValidator<TValidator>() where TValidator : FluentValidation.IValidator;
	}
}
