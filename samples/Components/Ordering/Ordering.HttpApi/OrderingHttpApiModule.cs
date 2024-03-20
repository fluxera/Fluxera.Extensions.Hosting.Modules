namespace Ordering.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Ordering.HttpApi.Contributors;

	[PublicAPI]
	[DependsOn<ODataModule>]
	[DependsOn<HttpApiModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class OrderingHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddEdmModelContributor<EdmModelContributor>();
		}

		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
