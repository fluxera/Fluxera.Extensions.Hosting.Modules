
namespace Catalog.Domain.Shared.Product.Events
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than an catalog was updated.
	/// </summary>
	[PublicAPI]
	public sealed class ProductUpdated : ItemUpdated
	{
	}
}
