//namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData.Contributor
//{
//	using JetBrains.Annotations;
//	using Microsoft.AspNetCore.Routing;

//	[UsedImplicitly]
//	internal sealed class ODataRouteEndpointContributor : IRouteEndpointContributor
//	{
//		/// <inheritdoc />
//		public void MapRoute(IEndpointRouteBuilder endpoints)
//		{
//			//// Create a logger.
//			//ILogger<ODataRouteEndpointContributor> logger = endpoints.ServiceProvider.GetRequiredService<ILogger<ODataRouteEndpointContributor>>();

//			//// Get the OData options.
//			//ODataOptions options = endpoints.ServiceProvider.GetRequiredService<IOptions<ODataOptions>>().Value;

//			//// Add OData route.
//			//IEdmModelBuilder modelBuilder = endpoints.ServiceProvider.GetRequiredService<IEdmModelBuilder>();
//			//IEdmModel[] models = modelBuilder.GetEdmModels();

//			////var modelBuilder = new VersionedODataModelBuilder()
//			////{
//			////	ModelConfigurations =
//			////	{
//			////		new PersonModelConfiguration()
//			////	}
//			////};
//			////var models = modelBuilder.GetEdmModels();

//			////// https://github.com/Microsoft/aspnet-api-versioning/wiki/Versioned-ODataModelBuilder
//			////routeBuilder.MapVersionedODataRoutes("odata", "v{version:apiVersion}", models);

//			//logger.LogDebug("\tUseEndpoints -> MapODataRoute");

//			//ODataBatchHandler batchHandler = new DefaultODataBatchHandler();
//			///*batchHandler.MessageQuotas.MaxNestingDepth = 2;
//			//batchHandler.MessageQuotas.MaxPartsPerBatch = 10;
//			//batchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
//			//batchHandler.MessageQuotas.MaxReceivedMessageSize = 100;*/

//			//endpoints.EnableDependencyInjection();
//			//endpoints.Select().Filter().OrderBy().Count().MaxTop(100).Expand();
//			//endpoints.MapODataRoute("OData", "odata", models.Single(), batchHandler);
//			//endpoints.SetTimeZoneInfo(TimeZoneInfo.Utc);
//		}
//	}
//}


