﻿namespace CatalogService
{
	using System.Net;
	using Catalog.Application;
	using Catalog.HttpApi;
	using Catalog.Infrastructure;
	using FluentValidation;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.ProblemDetails;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.HttpOverrides;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Serilog;

	[PublicAPI]
	[DependsOn<CatalogHttpApiModule>]
	[DependsOn<CatalogApplicationModule>]
	[DependsOn<CatalogInfrastructureModule>]
	public sealed class CatalogServiceModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Configure default exception mappings.
			context.Services.Configure<ProblemDetailsOptions>(options =>
			{
				options.MapStatusCode<ValidationException>(HttpStatusCode.BadRequest);
			});

			// Configure the forwarded headers middleware.
			context.Services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders = ForwardedHeaders.All;
			});
		}

		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			// Configure the HTTP request pipeline.
			if(context.Environment.IsDevelopment())
			{
				context.UseSwaggerUI();
			}

			context.UseForwardedHeaders();

			context.UseHsts();

			context.UseHttpsRedirection();

			context.UseRouting();

			context.UseEndpoints();
		}

		/// <inheritdoc />
		public override void OnApplicationShutdown(IApplicationShutdownContext context)
		{
			Log.CloseAndFlush();
		}
	}
}
