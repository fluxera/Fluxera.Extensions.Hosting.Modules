namespace BlazorWasmGlobal.Client
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using MadEyeMatt.AspNetCore.Blazor;

	internal class BlazorWasmGlobalClientModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddComponentActivator",
				services => services.AddComponentActivator());
		}
	}
}