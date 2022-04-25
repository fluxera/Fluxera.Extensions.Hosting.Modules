namespace WebSample.Contributors
{
	using Asp.Versioning;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData;
	using Microsoft.OData.ModelBuilder;
	using WebSample.Model;

	internal sealed class EdmModelContributor : IEdmModelContributor
	{
		private const string V1 = "1.0";
		private const string V2 = "2.0";

		/// <inheritdoc />
		public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
		{
			string version = apiVersion.ToString("VVVV");

			builder.Namespace = version switch
			{
				//V1 => "WebSample.v1.Model",
				//V2 => "WebSample.v2.Model",
				_ => "WebSample.Model"
			};

			builder.EntitySet<Person>("People");

			builder.EntityType<Person>()
				.HasKey(x => x.ID);
		}
	}
}
