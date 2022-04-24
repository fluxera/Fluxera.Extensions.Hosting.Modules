namespace WebSample.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.OData;
	using Microsoft.OData.ModelBuilder;
	using WebSample.Model;

	internal sealed class EdmModelContributor : IEdmModelContributor
	{
		/// <inheritdoc />
		public void Configure(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<Person>("People");

			builder.EntityType<Person>()
				.HasKey(x => x.ID);
		}
	}
}
