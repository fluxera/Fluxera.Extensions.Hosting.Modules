namespace BlazorWasmPerPageComponent.Client
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using MadEyeMatt.AspNetCore.Blazor;

	internal class BlazorWasmPerPageComponentClientModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddComponentActivator",
				services => services.AddComponentActivator());
		}
	}
}