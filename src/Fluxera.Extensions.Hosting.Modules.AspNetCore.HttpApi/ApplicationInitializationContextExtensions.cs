namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System.Linq;
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
		///     Register the SwaggerUI middleware with optional setup action for DI-injected options.
		/// </summary>
		public static IApplicationInitializationContext UseSwaggerUI(this IApplicationInitializationContext context)
		{
			HttpApiOptions httpApiOptions = context.ServiceProvider.GetRequiredService<IOptions<HttpApiOptions>>().Value;

			if(httpApiOptions.Swagger.Enabled)
			{
				WebApplication app = context.GetApplicationBuilder();
				context.Log("UseSwaggerUI", _ => app.UseSwaggerUI(options =>
				{
					if(httpApiOptions.Descriptions.Any())
					{
						foreach((string groupName, HttpApiDescription description) in httpApiOptions.Descriptions)
						{
							options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", $"{description.Title} V{description.Version.Major}");
						}
					}
				}));
			}

			return context;
		}
	}
}
