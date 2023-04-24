// ReSharper disable ReplaceWithSingleCallToSingle

namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using MadEyeMatt.MongoDB.DbContext;
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
		/// <returns></returns>
		public static IServiceCollection AddMongoDbContext(
			this IServiceCollection services,
			Type dbContextType,
			Action<MongoDbContextOptionsBuilder> optionsAction = null)
		{
			Guard.Against.Null(services);
			Guard.Against.Null(dbContextType);
			Guard.Against.False(dbContextType.IsAssignableTo<MongoDbContext>(), "The type to register must inherit from MongoDbContext");

			MethodInfo methodInfo = typeof(MadEyeMatt.MongoDB.DbContext.ServiceCollectionExtensions)
				.GetRuntimeMethods()
				.Where(x => x.Name == "AddMongoDbContext")
				.Where(x => x.IsGenericMethod)
				.Where(x => x.GetGenericArguments().Length == 1)
				.Where(x => x.GetParameters().Length == 2)
				.Where(x => x.GetParameters()[1].ParameterType == typeof(Action<MongoDbContextOptionsBuilder>))
				.Single();

			methodInfo
				.MakeGenericMethod(dbContextType)
				.Invoke(null, new object[]
				{
					services,
					optionsAction
				});

			return services;
		}
	}
}
