namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors
{
	using Asp.Versioning;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.AspNetCore.OData;
	using Microsoft.AspNetCore.OData.Batch;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.OData.Edm;
	using Microsoft.OData.ModelBuilder;
	using ODataOptions = OData.ODataOptions;

	internal sealed class MvcBuilderContributor : IMvcBuilderContributor
	{
		/// <inheritdoc />
		public void Configure(IMvcBuilder builder, IServiceConfigurationContext context)
		{
			HttpApiOptions httpApiOptions = context.Services.GetOptions<HttpApiOptions>();
			ODataOptions oDataOptions = context.Services.GetOptions<ODataOptions>();
			EdmModelContributorList contributorList = context.Services.GetObject<EdmModelContributorList>();
			ODataModelBuilder modelBuilder = new CustomODataModelBuilder();

			foreach(IEdmModelContributor edmModelContributor in contributorList)
			{
				edmModelContributor.Apply(modelBuilder, ApiVersion.Neutral, string.Empty);
			}

			IEdmModel edmModel = modelBuilder.GetEdmModel();

			builder.AddOData(options =>
			{
				options.EnableQueryFeatures();//.Count().Select().OrderBy();
				options.RouteOptions.EnableKeyInParenthesis = false;
				options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
				options.RouteOptions.EnableQualifiedOperationCall = false;
				options.RouteOptions.EnableUnqualifiedOperationCall = true;

				if(oDataOptions.Batching.Enabled)
				{
					DefaultODataBatchHandler batchHandler = new DefaultODataBatchHandler
					{
						PrefixName = "odata",
						MessageQuotas =
						{
							MaxNestingDepth = oDataOptions.Batching.MessageQuotas.MaxNestingDepth,
							MaxOperationsPerChangeset = oDataOptions.Batching.MessageQuotas.MaxOperationsPerChangeset,
							MaxPartsPerBatch = oDataOptions.Batching.MessageQuotas.MaxPartsPerBatch,
							MaxReceivedMessageSize = oDataOptions.Batching.MessageQuotas.MaxReceivedMessageSize
						}
					};
					options.AddRouteComponents("odata", edmModel, batchHandler);
				}
				else
				{
					options.AddRouteComponents("odata", edmModel);
				}
			});
		}
	}
}
