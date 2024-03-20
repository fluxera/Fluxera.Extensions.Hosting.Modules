namespace Catalog.Domain.Shared.ProductAggregate.Messages
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than a catalog was updated.
	/// </summary>
	[PublicAPI]
	public sealed class ProductUpdated : ItemUpdated
	{
	}
}
