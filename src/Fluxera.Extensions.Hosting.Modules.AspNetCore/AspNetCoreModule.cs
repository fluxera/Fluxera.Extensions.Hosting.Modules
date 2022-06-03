namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Enumeration.SystemTextJson;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Spatial.SystemTextJson;
	using Fluxera.StronglyTypedId.SystemTextJson;
	using Fluxera.ValueObject.SystemTextJson;
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
			context.Log("AddHttpContextAccessor", services => services.AddHttpContextAccessor());

			// Add the action context accessor.
			context.Log("AddActionContextAccessor", services => services.AddActionContextAccessor());

			// Add the controller services.
			IMvcBuilder builder = context.Log("AddControllers", services => services.AddControllers());

			// Add the modules as application parts.
			context.Log("AddApplicationParts", _ => builder.AddApplicationParts());

			// Add the discovered controllers as services.
			context.Log("AddControllersAsServices", _ => builder.AddControllersAsServices());

			// Configure the JSON serializer defaults.
			context.Log("AddJsonOptions", _ => builder.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.UseSpatial();
				options.JsonSerializerOptions.UseEnumeration();
				options.JsonSerializerOptions.UsePrimitiveValueObject();
				options.JsonSerializerOptions.UseStronglyTypedId();
			}));

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
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
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
