namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository;
	using Fluxera.Repository.Interception;

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

		/// <inheritdoc />
		public IInterceptorsBuilder AddInterceptors(IEnumerable<Type> types)
		{
			this.interceptionOptions.AddInterceptors(types);
			return this;
		}

		/// <inheritdoc />
		public IInterceptorsBuilder AddInterceptor(Type type)
		{
			this.interceptionOptions.AddInterceptor(type);
			return this;
		}

		/// <inheritdoc />
		public IInterceptorsBuilder AddInterceptor<TInterceptor>() where TInterceptor : IInterceptor
		{
			this.interceptionOptions.AddInterceptor<TInterceptor>();
			return this;
		}
	}
}
