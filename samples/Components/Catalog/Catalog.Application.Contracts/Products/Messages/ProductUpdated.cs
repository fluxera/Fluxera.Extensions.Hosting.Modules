namespace Catalog.Application.Contracts.Products.Messages
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than a catalog was updated.
	/// </summary>
	[PublicAPI]
	public sealed class ProductUpdated : ItemUpdated
	{
	}
}
