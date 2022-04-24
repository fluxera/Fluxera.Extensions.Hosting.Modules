namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables ASP.NET Core basic features.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(OpenTelemetryModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class AspNetCoreModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the tracer provider contributor.
			context.Services.AddTracerProviderContributor<TracerProviderContributor>();

			// Add the meter provider contributor.
			context.Services.AddMeterProviderContributor<MeterProviderContributor>();

			// Add the http context accessor.
			context.Log("AddHttpContextAccessor",
				services => services.AddHttpContextAccessor());

			// Add the action context accessor.
			context.Log("AddActionContextAccessor",
				services => services.AddActionContextAccessor());

			// Add the controller services.
			IMvcBuilder builder = context.Log("AddControllers",
				services => services.AddControllers());

			// Add discovered controllers as services.
			context.Log("AddControllersAsServices", _ => builder.AddControllersAsServices());

			// Add the modules as application parts.
			context.Log("AddApplicationParts", _ => builder.AddApplicationParts());

			// Add controllers as services.
			context.Log("AddControllersAsServices", _ => builder.AddControllersAsServices());

			// Add the mvc builder.
			context.Log("AddObjectAccessor(MvcBuilder)",
				services => services.AddObjectAccessor(new MvcBuilderContainer(builder), ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(MvcBuilderContributorList)",
				services => services.AddObjectAccessor(new MvcBuilderContributorList(), ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(RouteEndpointContributorList)",
				services => services.AddObjectAccessor(new RouteEndpointContributorList(), ObjectAccessorLifetime.Configure));

			// Add the route endpoint contributor.
			context.Services.AddRouteEndpointContributor<RouteEndpointContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the mvc builder.
			MvcBuilderContainer container = context.Services.GetObject<MvcBuilderContainer>();
			MvcBuilderContributorList contributorList = context.Services.GetObject<MvcBuilderContributorList>();
			foreach(IMvcBuilderContributor contributor in contributorList)
			{
				contributor.Configure(container.Builder, context);
			}
		}
	}
}
