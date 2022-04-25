namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData
{
	using JetBrains.Annotations;
	using Microsoft.OData.Edm;
	using Microsoft.OData.ModelBuilder;

	/// <summary>
	///     A contract for contributors that add <see cref="IEdmModel" /> instances to the service.
	/// </summary>
	[PublicAPI]
	public interface IEdmModelContributor
	{
		/// <summary>
		///     Configures <see cref="IEdmModel" /> instances in the given builder.
		/// </summary>
		/// <param name="builder"></param>
		void Configure(ODataConventionModelBuilder builder);
	}
}
