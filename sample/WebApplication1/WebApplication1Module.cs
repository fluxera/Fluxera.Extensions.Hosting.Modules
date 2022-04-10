namespace WebApplication1
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class WebApplication1Module : ConfigureApplicationModule
	{
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
