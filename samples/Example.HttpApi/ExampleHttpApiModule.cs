namespace Example.HttpApi
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn<HttpApiModule>]
	[DependsOn<ODataModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class ExampleHttpApiModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.UseProblemDetails();
		}
	}
}
