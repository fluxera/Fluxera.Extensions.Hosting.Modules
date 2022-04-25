namespace WebSample
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Swagger;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.OData;
	using Microsoft.Extensions.Hosting;
	using WebSample.Contributors;

	[PublicAPI]
	[DependsOn(typeof(InMemoryPersistenceModule))]
	[DependsOn(typeof(JwtBearerAuthenticationModule))]
	[DependsOn(typeof(AuthenticationModule))]
	[DependsOn(typeof(AuthorizationModule))]
	//[DependsOn(typeof(VersioningModule))]
	//[DependsOn(typeof(ODataVersioningModule))]
	[DependsOn(typeof(SwaggerModule))]
	[DependsOn(typeof(WarmupModule))]
	[DependsOn(typeof(HealthChecksModule))]
	[DependsOn(typeof(ODataModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class WebSampleModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddRepositoryContributor<RepositoryContributor>("Test");
			context.Services.AddEdmModelContributor<EdmModelContributor>();
		}

		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			// Configure the HTTP request pipeline.
			if(context.Environment.IsDevelopment())
			{
				context.UseSwagger();
				context.UseSwaggerUI();
			}

			context.UseHttpsRedirection();

			context.UseCors();

			context.GetApplicationBuilder().UseODataRouteDebug();

			context.UseRouting();

			context.UseAuthentication();
			context.UseAuthorization();

			context.UseEndpoints();
		}
	}
}
