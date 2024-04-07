namespace Ordering.HttpApi.Contributors
{
	using Asp.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Microsoft.OData.ModelBuilder;
	using Ordering.Application.Contracts.Orders;

	public sealed class EdmModelContributor : IEdmModelContributor
	{
		/// <inheritdoc />
		public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
		{
			builder.EntitySet<OrderDto>("Orders").EntityType.HasID(x => x.EntityId);
		}
	}
}
