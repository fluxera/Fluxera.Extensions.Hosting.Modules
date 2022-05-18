namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using FluentValidation;
	using Fluxera.Extensions.Validation.FluentValidation;

	internal sealed class ValidatorsBuilder : IValidatorsBuilder
	{
		private readonly ValidatorRegistration validatorRegistration;

		public ValidatorsBuilder(ValidatorRegistration validatorRegistration)
		{
			this.validatorRegistration = validatorRegistration;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidators(IEnumerable<Assembly> assemblies)
		{
			this.validatorRegistration.AddValidators(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidators(Assembly assembly)
		{
			this.validatorRegistration.AddValidators(assembly);
			return this;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidators(IEnumerable<Type> types)
		{
			this.validatorRegistration.AddValidators(types);
			return this;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidator(Type type)
		{
			this.validatorRegistration.AddValidator(type);
			return this;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidator<TValidator>() where TValidator : IValidator
		{
			this.validatorRegistration.AddValidator<TValidator>();
			return this;
		}
	}
}
