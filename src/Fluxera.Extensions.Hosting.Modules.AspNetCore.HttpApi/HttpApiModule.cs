namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Asp.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enabled HTTP APIs.
	/// </summary>
	/// <remarks>
	///     https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters
	///     https://github.com/unchase/Unchase.Swashbuckle.AspNetCore.Extensions
	///     https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation
	/// </remarks>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class HttpApiModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the swagger route contributor.
			context.Services.AddRouteEndpointContributor<RouteEndpointContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			HttpApiOptions httpApiOptions = context.Services.GetOptions<HttpApiOptions>();

			// Add API versioning.
			if(httpApiOptions.IsVersioningEnabled)
			{
				context.Log("AddApiVersioning", services =>
				{
					// https://github.com/dotnet/aspnet-api-versioning/wiki
					IApiVersioningBuilder versioningBuilder = services.AddApiVersioning(options =>
					{
						options.DefaultApiVersion = new ApiVersion(httpApiOptions.DefaultVersion.Major, httpApiOptions.DefaultVersion.Major);
						options.AssumeDefaultVersionWhenUnspecified = true;
						options.ReportApiVersions = true;
					});

					versioningBuilder.AddMvc();

					if(httpApiOptions.Swagger.Enabled)
					{
						versioningBuilder.AddApiExplorer(options =>
						{
							options.GroupNameFormat = "'v'VVV";
							options.SubstituteApiVersionInUrl = true;
						});
					}
				});
			}

			// Configure swagger filters.
			if(httpApiOptions.Swagger.Enabled)
			{
				context.Log("AddSwagger", services =>
				{
					// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
					services.AddEndpointsApiExplorer();
					services.AddSwaggerGen(options =>
					{
						options.OperationFilter<SwaggerDefaultValuesFilter>();
						options.OperationFilter<DefaultValuesFilter>();
						options.OperationFilter<DeprecatedOperationFilter>();

						options.EnableAnnotations();
						options.UseInlineDefinitionsForEnums();
						options.IncludeXmlComments();
					});

					services.ConfigureOptions<ConfigureSwaggerOptions>();

					if(httpApiOptions.IsVersioningEnabled)
					{
						services.ConfigureOptions<VersioningConfigureSwaggerOptions>();
					}
				});
			}
		}
	}
}
