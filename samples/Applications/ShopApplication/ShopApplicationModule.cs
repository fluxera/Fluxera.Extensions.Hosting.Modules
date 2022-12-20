namespace ShopApplication
{
	using System.Linq;
	using Catalog.Application;
	using Catalog.Application.Contracts.Services;
	using Catalog.HttpClient;
	using Catalog.MessagingApi;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.RazorPages;
	using Fluxera.Extensions.Hosting.Modules.MultiTenancy;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Hosting;
	using Ordering.Application;
	using Ordering.HttpClient;
	using Ordering.MessagingApi;

	[PublicAPI]
	[DependsOn<RazorPagesModule>]
	//[DependsOn<MultiTenancyModule>]
	// Configure the Catalog component.
	//[DependsOn<CatalogMessagingApiModule>]
	//[DependsOn<CatalogApplicationModule>]
	// Configure the Ordering component.
	//[DependsOn<OrderingMessagingApiModule>]
	//[DependsOn<OrderingApplicationModule>]
	// Configure the HTTP client modules.
	//[DependsOn<CatalogHttpApiModule>]
	//[DependsOn<OrderingHttpApiModule>]
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
