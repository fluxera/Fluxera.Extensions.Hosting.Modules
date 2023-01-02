namespace ShopApplication
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.RazorPages;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Hosting;

	[PublicAPI]
	[DependsOn<RazorPagesModule>]
	[DependsOn<HealthChecksEndpointsModule>]
	public sealed class ShopApplicationModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			// Configure the HTTP request pipeline.
			if(!context.Environment.IsDevelopment())
			{
				context.UseExceptionHandler("/Error");

				context.UseHsts();
			}

			context.UseHttpsRedirection();

			context.UseStaticFiles();

			context.UseRouting();

			context.UseEndpoints();
		}
	}
}
