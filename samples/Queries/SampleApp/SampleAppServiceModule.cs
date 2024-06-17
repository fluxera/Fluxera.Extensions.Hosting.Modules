namespace SampleApp
{
	using System.Net;
	using FluentValidation;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.ProblemDetails;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using SampleApp.Application;
	using SampleApp.HttpApi;
	using SampleApp.Infrastructure;
	using Serilog;

	[UsedImplicitly]
	[DependsOn<SampleAppApplicationModule>]
	[DependsOn<SampleAppInfrastructureModule>]
	[DependsOn<SampleAppHttpApiModule>]
	internal sealed class SampleAppServiceModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Configure default exception mappings.
			context.Services.Configure<ProblemDetailsOptions>(options =>
			{
				options.MapStatusCode<ValidationException>(HttpStatusCode.BadRequest);
			});
        }

		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			// Configure the HTTP request pipeline.
			if (context.Environment.IsDevelopment())
			{
				context.UseSwaggerUI();
			}

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
