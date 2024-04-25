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
		public IInterceptorsBuilder AddInterceptors(IEnumerable<Assembly> assemblies)
		{
			this.interceptionOptions.AddInterceptors(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IInterceptorsBuilder AddInterceptors(Assembly assembly)
		{
			this.interceptionOptions.AddInterceptors(assembly);
			return this;
		}
	}
}
