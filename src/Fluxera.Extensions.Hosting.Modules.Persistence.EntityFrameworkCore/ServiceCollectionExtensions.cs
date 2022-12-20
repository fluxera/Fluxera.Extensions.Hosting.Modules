// ReSharper disable ReplaceWithSingleCallToSingle

namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Registers the given context as a service in the <see cref="IServiceCollection" />.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="dbContextType"></param>
		/// <param name="optionsAction"></param>
		/// <param name="contextLifetime"></param>
		/// <param name="optionsLifetime"></param>
		/// <returns></returns>
		public static IServiceCollection AddDbContext(
			this IServiceCollection services,
			Type dbContextType,
			Action<DbContextOptionsBuilder> optionsAction = null,
			ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
			ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
		{
			Guard.Against.Null(services);
			Guard.Against.Null(dbContextType);
			Guard.Against.False(dbContextType.IsAssignableTo<DbContext>(), "The type to register must inherit from DbContext");

			MethodInfo methodInfo = typeof(EntityFrameworkServiceCollectionExtensions)
				.GetRuntimeMethods()
				.Where(x => x.Name == "AddDbContext")
				.Where(x => x.IsGenericMethod)
				.Where(x => x.GetGenericArguments().Length == 1)
				.Where(x => x.GetParameters().Length == 4)
				.Where(x => x.GetParameters()[1].ParameterType == typeof(Action<DbContextOptionsBuilder>))
				.Where(x => x.GetParameters()[2].ParameterType == typeof(ServiceLifetime))
				.Where(x => x.GetParameters()[3].ParameterType == typeof(ServiceLifetime))
				.Single();

			methodInfo
				.MakeGenericMethod(dbContextType)
				.Invoke(null, new object[]
				{
					services,
					optionsAction,
					contextLifetime,
					optionsAction
				});

			return services;
		}
	}
}
