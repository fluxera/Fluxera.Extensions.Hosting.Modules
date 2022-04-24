namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData.Contributor
{
	using Fluxera.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.OData;
	using Microsoft.AspNetCore.OData.Batch;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.OData.ModelBuilder;

	internal sealed class MvcBuilderContributor : IMvcBuilderContributor
	{
		/// <inheritdoc />
		public void Configure(IMvcBuilder builder, IServiceConfigurationContext context)
		{
			EdmModelContributorList contributorList = context.Services.GetObject<EdmModelContributorList>();
			ODataConventionModelBuilder modelBuilder = new CustomODataModelBuilder();

			builder.AddOData(option =>
			{
				option.Select().Filter().Count().OrderBy().Expand().SkipToken().SetMaxTop(100);

				foreach(IEdmModelContributor edmModelContributor in contributorList)
				{
					edmModelContributor.Configure(modelBuilder);

					option.AddRouteComponents("odata", modelBuilder.GetEdmModel(), new DefaultODataBatchHandler());
				}
			});
		}
	}
}
