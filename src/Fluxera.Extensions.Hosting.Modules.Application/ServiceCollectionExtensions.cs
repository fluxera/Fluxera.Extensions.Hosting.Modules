namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extensions to scan for MediatR handlers and register them.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Registers handlers and mediator types from the executing assembly.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMediatR(this IServiceCollection services)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();

			return services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssemblies(callingAssembly);
			});
		}
	}
}
