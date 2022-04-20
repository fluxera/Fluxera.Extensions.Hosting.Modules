namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	public interface IValidatorBuilder
	{
		IValidatorBuilder AddValidators(IEnumerable<Assembly> assemblies);

		IValidatorBuilder AddValidators(Assembly assembly);

		IValidatorBuilder AddValidators(IEnumerable<Type> types);

		IValidatorBuilder AddValidator(Type type);

		IValidatorBuilder AddValidator<TValidator>() where TValidator : FluentValidation.IValidator;
	}
}
