namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.AspNetCore.OData;
	using Microsoft.AspNetCore.OData.Batch;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.OData.ModelBuilder;

	internal sealed class MvcBuilderContributor : IMvcBuilderContributor
	{
		/// <inheritdoc />
		public void Configure(IMvcBuilder builder, IServiceConfigurationContext context)
		{
			OData.ODataOptions oDataOptions = context.Services.GetOptions<OData.ODataOptions>();
			EdmModelContributorList contributorList = context.Services.GetObject<EdmModelContributorList>();
			ODataConventionModelBuilder modelBuilder = new CustomODataModelBuilder();

			builder.AddOData(options =>
			{
				options.Select().Filter().Count().OrderBy().Expand().SkipToken().SetMaxTop(100);

				foreach(IEdmModelContributor edmModelContributor in contributorList)
				{
					edmModelContributor.Configure(modelBuilder);

					if(oDataOptions.Batching.Enabled)
					{
						DefaultODataBatchHandler batchHandler = new DefaultODataBatchHandler
						{
							PrefixName = "api",
							MessageQuotas =
							{
								MaxNestingDepth = oDataOptions.Batching.MessageQuotas.MaxNestingDepth,
								MaxOperationsPerChangeset = oDataOptions.Batching.MessageQuotas.MaxOperationsPerChangeset,
								MaxPartsPerBatch = oDataOptions.Batching.MessageQuotas.MaxPartsPerBatch,
								MaxReceivedMessageSize = oDataOptions.Batching.MessageQuotas.MaxReceivedMessageSize
							}
						};
						options.AddRouteComponents("api", modelBuilder.GetEdmModel(), batchHandler);
					}
					else
					{
						options.AddRouteComponents("api", modelBuilder.GetEdmModel());
					}
				}
			});
		}
	}
}
