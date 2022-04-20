namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables ASP.NET Core basic features.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class AspNetCoreModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the http context accessor.
			context.Log("AddHttpContextAccessor",
				services => services.AddHttpContextAccessor());

			// Add the action context accessor.
			context.Log("AddActionContextAccessor",
				services => services.AddActionContextAccessor());

			// Add the controller services.
			IMvcBuilder builder = context.Log("AddControllers",
				services => services.AddControllers());

			// Add the modules as application parts.
			context.Log("AddApplicationParts", _ => builder.AddApplicationParts());

			// Add controllers as services.
			context.Log("AddControllersAsServices", _ => builder.AddControllersAsServices());

			// Add the mvc builder.
			context.Log("AddObjectAccessor(MvcBuilder)",
				services => services.AddObjectAccessor(builder, ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(MvcBuilderContributorList)",
				services => services.AddObjectAccessor(new MvcBuilderContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the mvc builder.
			IMvcBuilder builder = context.Services.GetObject<IMvcBuilder>();
			MvcBuilderContributorList contributorList = context.Services.GetObject<MvcBuilderContributorList>();
			foreach(IMvcBuilderContributor contributor in contributorList)
			{
				contributor.Configure(builder);
			}
		}
	}
}
