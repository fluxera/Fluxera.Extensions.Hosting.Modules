namespace Fluxera.Extensions.Hosting.Modules.MediatR
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables MediatR.
	/// </summary>
	[PublicAPI]
	[DependsOn<ConfigurationModule>]
	public sealed class MediatrModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the contributor list.
			context.Log("AddObjectAccessor(MediatrContributorList)",
				services => services.AddObjectAccessor(new MediatrContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Get the module container.
			IModuleContainer moduleContainer = context.Services.GetObject<IModuleContainer>();

			// Get all distinct module assemblies.
			Assembly[] assemblies = moduleContainer.Modules.Select(x => x.Assembly).Distinct().ToArray();

			context.Log("AddMediatR", services =>
			{
				MediatRServiceConfiguration configuration = new MediatRServiceConfiguration();

				// Add all available validators from the modules.
				configuration.RegisterServicesFromAssemblies(assemblies);

				// Add additional MediatR configuration.
				MediatrContributorList contributorList = context.Services.GetObject<MediatrContributorList>();
				foreach(IMediatrContributor contributor in contributorList)
				{
					contributor.Configure(context, configuration);
				}

				services.AddMediatR(configuration);
			});

		}
	}
}
