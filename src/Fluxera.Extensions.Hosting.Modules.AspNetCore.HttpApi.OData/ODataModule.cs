namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System.Collections.Generic;
	using Asp.Versioning;
	using Asp.Versioning.OData;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Caching;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.OData.Batch;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enables OData REST APIs.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(CachingModule))]
	[DependsOn(typeof(HttpApiModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class ODataModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the authorize contributor.
			context.Services.AddAuthorizeContributor<AuthorizeContributor>();

			// Add the mvc builder contributor.
			context.Services.AddMvcBuilderContributor<MvcBuilderContributor>();

			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the contributor list.
			context.Log("AddObjectAccessor(EdmModelContributorList)",
				services => services.AddObjectAccessor(new EdmModelContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			HttpApiOptions httpApiOptions = context.Services.GetOptions<HttpApiOptions>();
			ODataOptions oDataOptions = context.Services.GetOptions<ODataOptions>();

			// Add OData API versioning.
			if(httpApiOptions.Versioning.Enabled)
			{
				context.Log("AddApiVersioning", services =>
				{
					services.TryAddTransient(sp =>
					{
						VersionedODataModelBuilder modelBuilder = new VersionedODataModelBuilder(
							sp.GetRequiredService<IODataApiVersionCollectionProvider>(),
							sp.GetRequiredService<IEnumerable<IModelConfiguration>>())
						{
							ModelBuilderFactory = () => new CustomODataModelBuilder()
						};

						return modelBuilder;
					});

					// https://github.com/dotnet/aspnet-api-versioning/wiki
					IApiVersioningBuilder versioningBuilder = services.AddApiVersioning(options =>
					{
						options.DefaultApiVersion = new ApiVersion(httpApiOptions.DefaultVersion.Major, httpApiOptions.DefaultVersion.Major);
						options.AssumeDefaultVersionWhenUnspecified = true;
						options.ReportApiVersions = true;
					});

					versioningBuilder.AddOData(options =>
					{
						if(oDataOptions.Batching.Enabled)
						{
							DefaultODataBatchHandler batchHandler = new DefaultODataBatchHandler
							{
								PrefixName = "v{version:apiVersion}",
								MessageQuotas =
								{
									MaxNestingDepth = oDataOptions.Batching.MessageQuotas.MaxNestingDepth,
									MaxOperationsPerChangeset = oDataOptions.Batching.MessageQuotas.MaxOperationsPerChangeset,
									MaxPartsPerBatch = oDataOptions.Batching.MessageQuotas.MaxPartsPerBatch,
									MaxReceivedMessageSize = oDataOptions.Batching.MessageQuotas.MaxReceivedMessageSize
								}
							};

							options.AddRouteComponents("v{version:apiVersion}", batchHandler);
						}
						else
						{
							options.AddRouteComponents("v{version:apiVersion}");
						}
					});

					if(httpApiOptions.Swagger.Enabled)
					{
						versioningBuilder.AddODataApiExplorer(options =>
						{
							options.GroupNameFormat = "'v'VVV";
							options.SubstituteApiVersionInUrl = true;
						});
					}
				});
			}

			//// Configure swagger filters.
			//if(httpApiOptions.Swagger.Enabled)
			//{
			//	context.Services.Configure<SwaggerGenOptions>(options =>
			//	{
			//		options.DocumentFilter<ODataMetadataControllerDocumentFilter>();
			//	});
			//}

			//context.Services.AddODataQueryFilter();

			// Add the idempotent token filter.
			context.Log("AddIdempotentTokenFilter",
				services => services.TryAddScoped<IdempotentTokenFilter>());

			// TODO: Change the base address used by OData to create link refs.
			//context.Services.Configure<MvcOptions>(options =>
			//{
			//	foreach (IOutputFormatter formatter in options.OutputFormatters)
			//	{
			//		if (formatter is ODataOutputFormatter outputFormatter)
			//		{
			//			outputFormatter.BaseAddressFactory = request => new Uri("https://api.fluxera.com/odata");
			//		}
			//	}
			//});
		}
	}
}
