namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System.Linq;
	using Asp.Versioning.ApiExplorer;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
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
			IApiVersionDescriptionProvider versionDescriptionProvider = context.ServiceProvider.GetService<IApiVersionDescriptionProvider>();
			IHostEnvironment environment = context.ServiceProvider.GetRequiredService<IHostEnvironment>();
			HttpApiOptions httpApiOptions = context.ServiceProvider.GetRequiredService<IOptions<HttpApiOptions>>().Value;

			if(httpApiOptions.Swagger.Enabled)
			{
				WebApplication app = context.GetApplicationBuilder();
				context.Log("UseSwaggerUI", _ => app.UseSwaggerUI(options =>
				{
					if((versionDescriptionProvider != null) && versionDescriptionProvider.ApiVersionDescriptions.Any())
					{
						foreach(ApiVersionDescription description in versionDescriptionProvider.ApiVersionDescriptions)
						{
							string url = $"/swagger/{description.GroupName}/swagger.json";
							string name = description.GroupName.ToUpperInvariant();
							options.SwaggerEndpoint(url, name);
						}
					}
					else
					{
						options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
					}
				}));
			}

			return context;
		}
	}
}
