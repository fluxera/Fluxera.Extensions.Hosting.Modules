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
		public IValidatorsBuilder AddValidatorsFromAssemblies(IEnumerable<Assembly> assemblies)
		{
			this.validationOptionsBuilder.AddValidatorsFromAssemblies(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IValidatorsBuilder AddValidatorsFromAssembly(Assembly assembly)
		{
			this.validationOptionsBuilder.AddValidatorsFromAssembly(assembly);
			return this;
		}
	}
}
