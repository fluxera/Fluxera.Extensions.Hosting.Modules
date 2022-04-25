namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Swagger
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Extension methods for the <see cref="IApplicationInitializationContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ApplicationInitializationContextExtensions
	{
		/// <summary>
		///     Register the Swagger middleware with optional setup action for DI-injected options.
		/// </summary>
		public static IApplicationInitializationContext UseSwagger(this IApplicationInitializationContext context)
		{
			SwaggerOptions options = context.ServiceProvider.GetRequiredService<IOptions<SwaggerOptions>>().Value;
			if(options.Enabled)
			{
				WebApplication app = context.GetApplicationBuilder();
				context.Log("UseSwagger", _ => app.UseSwagger());
			}

			return context;
		}

		/// <summary>
		///     Register the SwaggerUI middleware with optional setup action for DI-injected options.
		/// </summary>
		public static IApplicationInitializationContext UseSwaggerUI(this IApplicationInitializationContext context)
		{
			SwaggerOptions swaggerOptions = context.ServiceProvider.GetRequiredService<IOptions<SwaggerOptions>>().Value;

			if(swaggerOptions.Enabled)
			{
				WebApplication app = context.GetApplicationBuilder();
				context.Log("UseSwaggerUI", _ => app.UseSwaggerUI(options =>
				{
					//if(swaggerOptions.Descriptions.Any())
					//{
					//	foreach((string groupName, SwaggerApiDescription _) in swaggerOptions.Descriptions)
					//	{
					//		options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
					//	}
					//}
					//else
					//{
					//	options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
					//}
				}));
			}

			return context;
		}
	}
}
