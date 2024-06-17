namespace SampleApp.Application
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using SampleApp.Application.Contracts.Customers;
	using SampleApp.Application.Contributors;
	using SampleApp.Application.Customers;
	using SampleApp.Domain;

	/// <summary>
	///     The application module.
	/// </summary>
	[PublicAPI]
	[DependsOn<SampleAppDomainModule>]
	[DependsOn<ApplicationModule>]
	public sealed class SampleAppApplicationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the mapping profile contributor.
			context.Services.AddMappingProfileContributor<MappingProfileContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add application services.
			context.Log("AddApplicationServices", services =>
			{
				services.AddTransient<ICustomersApplicationService, CustomersApplicationService>();
			});
		}
	}
}
