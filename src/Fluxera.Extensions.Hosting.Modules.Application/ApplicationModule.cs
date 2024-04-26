namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Application.Behaviors;
	using Fluxera.Extensions.Hosting.Modules.Application.Contributors;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using Fluxera.Extensions.Hosting.Modules.FluentValidation;
	using JetBrains.Annotations;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	///     The application module.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	[DependsOn<AutoMapperModule>]
	[DependsOn<FluentValidationModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class ApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add mapping contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Get the module container.
			IModuleContainer moduleContainer = context.Services.GetObject<IModuleContainer>();

			// Get all distinct module assemblies.
			Assembly[] assemblies = moduleContainer.Modules.Select(x => x.Assembly).Distinct().ToArray();

			// Add the MediatR services.
			context.Log("AddMediatR", services =>
			{
				// Add MediatR including all handlers available in modules.
				services.AddMediatR(config =>
				{
					config.MediatorImplementationType = typeof(NotificationValidatingMediator);
					config.RegisterServicesFromAssemblies(assemblies);
				});

				// Add the validation pipeline behavior for MediatR.
				services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
			});
		}
	}
}
