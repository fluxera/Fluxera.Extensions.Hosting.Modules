namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository;
	using Fluxera.Repository.Interception;

	internal sealed class InterceptionBuilder : IInterceptionBuilder
	{
		private readonly IInterceptionOptionsBuilder interceptionOptions;

		public InterceptionBuilder(IInterceptionOptionsBuilder interceptionOptions)
		{
			this.interceptionOptions = interceptionOptions;
		}

		/// <inheritdoc />
		public IInterceptionBuilder AddInterceptors(IEnumerable<Assembly> assemblies)
		{
			this.interceptionOptions.AddInterceptors(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IInterceptionBuilder AddInterceptors(Assembly assembly)
		{
			this.interceptionOptions.AddInterceptors(assembly);
			return this;
		}

		/// <inheritdoc />
		public IInterceptionBuilder AddInterceptors(IEnumerable<Type> types)
		{
			this.interceptionOptions.AddInterceptors(types);
			return this;
		}

		/// <inheritdoc />
		public IInterceptionBuilder AddInterceptor(Type type)
		{
			this.interceptionOptions.AddInterceptor(type);
			return this;
		}

		/// <inheritdoc />
		public IInterceptionBuilder AddInterceptor<TInterceptor>() where TInterceptor : IInterceptor
		{
			this.interceptionOptions.AddInterceptor<TInterceptor>();
			return this;
		}
	}
}
