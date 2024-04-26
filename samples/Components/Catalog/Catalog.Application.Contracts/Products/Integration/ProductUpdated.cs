namespace Catalog.Application.Contracts.Products.Integration
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than a catalog was updated.
	/// </summary>
	[PublicAPI]
	public sealed class ProductUpdated : IIntegrationEvent
	{
	}
}
