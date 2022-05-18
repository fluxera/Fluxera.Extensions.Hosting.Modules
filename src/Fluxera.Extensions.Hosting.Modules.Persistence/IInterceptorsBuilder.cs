namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository.Interception;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IInterceptorsBuilder
	{
		IInterceptorsBuilder AddInterceptors(IEnumerable<Assembly> assemblies);

		IInterceptorsBuilder AddInterceptors(Assembly assembly);

		IInterceptorsBuilder AddInterceptors(IEnumerable<Type> types);

		IInterceptorsBuilder AddInterceptor(Type type);

		IInterceptorsBuilder AddInterceptor<TInterceptor>() where TInterceptor : IInterceptor;
	}
}
