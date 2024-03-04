namespace BlazorWasmGlobal
{
	using BlazorWasmGlobal.Components;
	using BlazorWasmGlobal.Contributors;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor;
	using Microsoft.Extensions.Hosting;

	[DependsOn<BlazorModule>]
	public sealed class BlazorWasmGlobalModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddBlazorAssembliesContributor<BlazorAssembliesContributor>();
		}

		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			// Configure the HTTP request pipeline.
			if(context.Environment.IsDevelopment())
			{
				context.UseWebAssemblyDebugging();
			}
			else
			{
				context.UseExceptionHandler("/Error");
				context.UseHsts();
			}

			context.UseHttpsRedirection();

			context.UseStaticFiles();

			context.UseAntiforgery();

			context.UseRazorComponents<App>();
		}
	}
}
