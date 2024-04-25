namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System.Reflection;
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Behaviors;
	using JetBrains.Annotations;
	using MediatR;
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
			Assembly executingAssembly = Assembly.GetExecutingAssembly();

			// Add the MediatR services.
			services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssemblies(callingAssembly);
				config.RegisterServicesFromAssemblies(executingAssembly);
			});

			// Add the validation pipeline behavior for MediatR.
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

			// Add the validators.
			services.AddValidatorsFromAssembly(callingAssembly, includeInternalTypes: true);
			services.AddValidatorsFromAssembly(executingAssembly, includeInternalTypes: true);

			return services;
		}
	}
}
