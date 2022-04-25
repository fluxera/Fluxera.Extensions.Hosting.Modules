namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using Asp.Versioning.OData;
	using JetBrains.Annotations;
	using Microsoft.OData.Edm;

	/// <summary>
	///     A contract for contributors that add <see cref="IEdmModel" /> instances to the service.
	/// </summary>
	[PublicAPI]
	public interface IEdmModelContributor : IModelConfiguration
	{
	}
}
