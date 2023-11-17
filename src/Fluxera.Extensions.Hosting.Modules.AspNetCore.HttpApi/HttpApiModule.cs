namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Asp.Versioning;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using MadEyeMatt.AspNetCore.ProblemDetails;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables HTTP APIs.
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
			context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();

			// Add the problem details mvc builder contributor.
			context.Services.AddMvcBuilderContributor<MvcBuilderContributor>();

			// Add the contributor list.
			context.Log("AddObjectAccessor(ProblemDetailsContributorList)",
				services => services.AddObjectAccessor(new ProblemDetailsContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			HttpApiOptions httpApiOptions = context.Services.GetOptions<HttpApiOptions>();

			// Add API versioning.
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

				versioningBuilder.AddApiExplorer(options =>
				{
					options.GroupNameFormat = "'v'VVV";
					options.SubstituteApiVersionInUrl = true;
				});
			});

			// Configure swagger.
			context.Log("AddSwagger", services =>
			{
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
			});

			// Configure swagger for endpoint groups.
			context.Log("ConfigureEndpointsOptions", services =>
			{
				services.Configure<EndpointsOptions>(options =>
				{
					options.MapGroup = groupBuilder =>
					{
						groupBuilder.WithOpenApi();
					};
				});
			});
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// https://github.com/mgernand/AspNetCore.ProblemDetails/blob/main/samples/AspNetCore.ProblemDetails.Demo/Program.cs

			// Configure the problem details options.
			ProblemDetailsContributorList contributorList = context.Services.GetObject<ProblemDetailsContributorList>();
			foreach(IProblemDetailsContributor contributor in contributorList)
			{
				context.Services.Configure<ProblemDetailsOptions>(options =>
				{
					contributor.Configure(options, context);
				});
			}
		}
	}
}
