namespace Catalog.Application.Contracts.Products.Integration
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than an example was added.
	/// </summary>
	[PublicAPI]
	public sealed class ProductAdded : IIntegrationEvent
	{
	}
}
