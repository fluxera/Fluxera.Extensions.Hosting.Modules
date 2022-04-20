namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository.Interception;

	public interface IInterceptionBuilder
	{
		IInterceptionBuilder AddInterceptors(IEnumerable<Assembly> assemblies);

		IInterceptionBuilder AddInterceptors(Assembly assembly);

		IInterceptionBuilder AddInterceptors(IEnumerable<Type> types);

		IInterceptionBuilder AddInterceptor(Type type);

		IInterceptionBuilder AddInterceptor<TInterceptor>() where TInterceptor : IInterceptor;
	}
}
