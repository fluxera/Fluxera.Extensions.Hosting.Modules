namespace WebApplication1
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;
	using JetBrains.Annotations;
	using WebApplication1.Contributors;

	[PublicAPI]
	[DependsOn(typeof(InMemoryPersistenceModule))]
	[DependsOn(typeof(SwaggerModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class WebApplication1Module : ConfigureApplicationModule
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

			//context.UseAuthorization();

			context.MapControllers();
		}
	}
}
