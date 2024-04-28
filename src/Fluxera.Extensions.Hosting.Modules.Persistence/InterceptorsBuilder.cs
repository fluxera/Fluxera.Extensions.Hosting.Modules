namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository;

	internal sealed class InterceptorsBuilder : IInterceptorsBuilder
	{
		private readonly IInterceptionOptionsBuilder interceptionOptions;

		public InterceptorsBuilder(IInterceptionOptionsBuilder interceptionOptions)
		{
			this.interceptionOptions = interceptionOptions;
		}

		/// <inheritdoc />
		public IInterceptorsBuilder AddInterceptorsFromAssemblies(IEnumerable<Assembly> assemblies)
		{
			this.interceptionOptions.AddInterceptorsFromAssemblies(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IInterceptorsBuilder AddInterceptorsFromAssembly(Assembly assembly)
		{
			this.interceptionOptions.AddInterceptorsFromAssembly(assembly);
			return this;
		}
	}
}
