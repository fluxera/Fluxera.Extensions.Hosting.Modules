namespace WebSample
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;
	using JetBrains.Annotations;
	using WebSample.Contributors;

	[PublicAPI]
	[DependsOn(typeof(InMemoryPersistenceModule))]
	[DependsOn(typeof(JwtBearerAuthenticationModule))]
	[DependsOn(typeof(AuthenticationModule))]
	[DependsOn(typeof(AuthorizationModule))]
	[DependsOn(typeof(SwaggerModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class WebSampleModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddRepositoryContributor<RepositoryContributor>("Test");
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
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

			context.UseAuthentication();
			context.UseAuthorization();

			context.MapControllers();
		}
	}
}
