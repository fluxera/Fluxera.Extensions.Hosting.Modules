namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables Swagger.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class SwaggerModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			SwaggerOptions swaggerOptions = context.Services.GetOptions<SwaggerOptions>();
			if(swaggerOptions.Enabled)
			{
				context.Log("AddSwagger", services =>
				{
					// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
					services.AddEndpointsApiExplorer();
					services.AddSwaggerGen(options =>
					{
						options.IncludeXmlComments();

						options.EnableAnnotations();
						options.UseInlineDefinitionsForEnums();
					});
					services.ConfigureOptions<ConfigureSwaggerOptions>();
				});
			}
		}
	}
}
