namespace WebApplication1
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;
	using JetBrains.Annotations;
	using WebApplication1.Contributors;

	[PublicAPI]
	[DependsOn(typeof(InMemoryPersistenceModule))]
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
			context.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			context.Services.AddEndpointsApiExplorer();
			context.Services.AddSwaggerGen();
		}

		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			WebApplication app = context.GetApplicationBuilder();

			// Configure the HTTP request pipeline.
			if(context.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
