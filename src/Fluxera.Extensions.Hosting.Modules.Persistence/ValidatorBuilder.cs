namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using FluentValidation;
	using Fluxera.Extensions.Validation.FluentValidation;

	internal sealed class ValidatorBuilder : IValidatorBuilder
	{
		private readonly ValidatorRegistration validatorRegistration;

		public ValidatorBuilder(ValidatorRegistration validatorRegistration)
		{
			this.validatorRegistration = validatorRegistration;
		}

		/// <inheritdoc />
		public IValidatorBuilder AddValidators(IEnumerable<Assembly> assemblies)
		{
			this.validatorRegistration.AddValidators(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IValidatorBuilder AddValidators(Assembly assembly)
		{
			this.validatorRegistration.AddValidators(assembly);
			return this;
		}

		/// <inheritdoc />
		public IValidatorBuilder AddValidators(IEnumerable<Type> types)
		{
			this.validatorRegistration.AddValidators(types);
			return this;
		}

		/// <inheritdoc />
		public IValidatorBuilder AddValidator(Type type)
		{
			this.validatorRegistration.AddValidator(type);
			return this;
		}

		/// <inheritdoc />
		public IValidatorBuilder AddValidator<TValidator>() where TValidator : IValidator
		{
			this.validatorRegistration.AddValidator<TValidator>();
			return this;
		}
	}
}
