namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository;

	internal sealed class ValidatorsBuilder : IValidatorsBuilder
	{
		private readonly IValidationOptionsBuilder validationOptionsBuilder;

		public ValidatorsBuilder(IValidationOptionsBuilder validationOptionsBuilder)
		{
			this.validationOptionsBuilder = validationOptionsBuilder;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidators(IEnumerable<Assembly> assemblies)
		{
			this.validationOptionsBuilder.AddValidators(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidators(Assembly assembly)
		{
			this.validationOptionsBuilder.AddValidators(assembly);
			return this;
		}
	}
}
